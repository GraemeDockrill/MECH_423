clc; clear;
originalImage = imread("snowman.png");


% Get the dimensions of the original image
[rows, cols, ~] = size(originalImage);

% Determine the scaling factor
if(max(rows, cols) < 300)
    scaleFactor = 1;
else
    scaleFactor = 300 / max(rows, cols);
end

% Resize the image
scaledImage = imresize(originalImage, scaleFactor);
%imshow(scaledImage);

load("MGSData.mat") % Color space information.
dither = 'nodither';
prelim1 = rgb2ind(scaledImage, MGSPalette, dither);
prelim1 = ind2rgb(prelim1, MGSPalette);

[~, prelim6] = rgb2ind(prelim1, 6);
prelim2 = rgb2ind(prelim1, [prelim6; 1, 1, 1]);
prelim2 = ind2rgb(prelim2, [prelim6; 1, 1, 1]);

prelim3 = rgb2ind(prelim2, MGSPalette);
prelim3 = ind2rgb(prelim3, MGSPalette);

reshapedImage = reshape(prelim3, [], 3);

[uniqueList,~,ic] = unique(reshapedImage, 'rows');
tally = accumarray(ic, 1);
colorSelect = [tally, uniqueList];

colorSelect = sortrows(colorSelect, 1, "ascend");

top6 = [colorSelect(1:6, 2:4); 1, 1, 1];

colorizedImage_ind = rgb2ind(prelim3, top6);
colorizedImage = ind2rgb(colorizedImage_ind, top6);

figure(1)
clf
imshow(colorizedImage);

%% Tell which sharpie colours are required
disp("The colours required to make this image are:");
for i = 1:length(top6)-1
    index = find(ismember(MGSPalette, top6(i,:), "rows"));
    sharpie = MGSNames(index);
    disp(sharpie);
end

%% Sort coordinates into arrays based on colour:
figure(2)
clf
hold on;

for i = 1:6
    if(top6(i, :) == [1, 1, 1])
    else
        reshapedImage = reshape(colorizedImage, [], 3);
        targetIndices = find(ismember(reshapedImage, top6(i, :), 'rows'));
        [y, x] = ind2sub(size(colorizedImage), targetIndices);
        colorPixelArray{i} = [x, y];
        scatter(x, -y + height(colorizedImage),[] , top6(i, :))
    end
end
hold off
    
%% Convert pixel coordinates into physical step coordinates
mmPerStep = 12*pi   ...     %mm / rev
            / 200;          %steps / rev


dotDensity = 3 / mmPerStep; % This turns the separation in mm to separation in steps
dotDensity = ceil(dotDensity);

lastIndex = 0;
speedConst = 4713;
speedFloor = hex2dec('FF00');

%dataPacketArray0(1, :) = [255, 0, 0, 0, 0, 0, 0, 0];
for i = 1:length(colorPixelArray)
    colorstepsArray{i} = colorPixelArray{i}.*dotDensity;
    
    dx = [diff(colorstepsArray{i}(:,1))];
    dy = [diff(colorstepsArray{i}(:,2))];
    L = sqrt(dx.^2 + dy.^2);
    
    xspeed = [speedConst; abs(speedConst.*L./(dx+0.001))];
    xspeed(xspeed >= 0xFFF0) = 0xFFF0;
    xspeed(xspeed < speedConst) = speedConst;
    xspeed =  uint16(xspeed);
    speedArray{i}(:, 1) = xspeed;

    yspeed = [speedConst; abs(speedConst.*L./(dy+0.001))];
    yspeed(yspeed >= 0xFFF0) = 0xFFF0;
    yspeed(yspeed < speedConst) = speedConst;
    yspeed = uint16(yspeed);
    speedArray{i}(:, 2) = yspeed;
    

    for j = 1:length(colorPixelArray{i})
        dataPacketArray0(j + lastIndex, :) = [255, 1, i-1, colorstepsArray{i}(j, 1)...
            colorstepsArray{i}(j, 2), xspeed(j), yspeed(j), 0];
    end
    lastIndex = j + lastIndex;
end

dataPacketArray0(end+1, :) = [255, 2, 0, 0, 0, 0, 0, 0];


%% Send this info over serial:
clear s;
COMport= 'COM3'; baudRate = 19200;
s = serialport(COMport, baudRate,  Timeout=6);

state = 0;

for i = 1:height(dataPacketArray0)
    startByte = dataPacketArray0(i, 1);
    cmdByte = dataPacketArray0(i, 2);
    colorByte = dataPacketArray0(i, 3);
    [DB0, DB1, esc0] = byteSplitterDB(dataPacketArray0(i,4), 0);
    [DB2, DB3, esc2] = byteSplitterDB(dataPacketArray0(i,5), 2);
    [DB4, DB5, esc4] = byteSplitterDB(dataPacketArray0(i,6), 4);
    [DB6, DB7, esc6] = byteSplitterDB(dataPacketArray0(i,7), 6);
    escByte = esc0 + esc2 + esc4 + esc6;

    packet(i, :) = [startByte, cmdByte, colorByte, DB0, DB1, DB2, DB3, DB4... 
        DB5, DB6, DB7, escByte];
    write(s, packet(i,:), 'uint8');

    pktBack(i) = read(s, 14, "uint8");
    if(pktBack(1) == 255)
       continue
    else
       break
    end
end

