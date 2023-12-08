function [MSB, LSB, escByteAdd] = byteSplitterDB(inputNumber, MSB_DataByteNumber)
%UNTITLED Summary of this function goes here
%   Detailed explanation goes here

% Bring within 16 bit range:    

num = bitand(inputNumber, 65535);
escByteAdd = 0;

MSB = bitshift(num, -8);
if(MSB == 255)
    escByteAdd = 2^(7-MSB_DataByteNumber);
    MSB = 0;
end

LSB = bitand(num, 0x00FF);
if(LSB == 255)
    escByteAdd = escByteAdd + 2^(6-MSB_DataByteNumber);
    LSB = 0;
end

end