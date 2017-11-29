________________________________________________________________________________________________________________________________________
Install
________________________________________________________________________________________________________________________________________
To use LPB Archive you need to have the following:
Source video files - 10bit – yCbCr uncompressed in a .mov container. 
Three programs (listed below).

FFMPEG - https://www.ffmpeg.org/download.html

Mediainfo CLI - https://mediaarea.net/en/MediaInfo/Download/Windows

Mediaconch CLI - https://mediaarea.net/MediaConch/downloads/windows.html



Place each of the executables in the same folder you placed the downloaded archive software. An example folder would be c:\archive\ . This is done to make the archive program a little more portable. You can specify a different folder for the above executables if it’s required. 

If the Archive program does not start, then there is a chance you need to right click on the file and click on properties and look for an “Unblock” button on the “General” tab.
 
________________________________________________________________________________________________________________________________________
Configuration
________________________________________________________________________________________________________________________________________
Once the program is loaded, specify the three folder locations. Please note a trailing \ needs on each of the paths.

Input Path – Folder where the 10bit YCbCr .mov files are located.

Output path – Where you want the files to be placed during/after processing.

Tools Path – Where the three executables mentioned above are located. 

Checking the “Delete unused master file” box will get rid one of the two master files depending on how the ffmpeg frame data comparison goes. Best case is you end up with a FFV1 .mkv file which is same quality as the uncompressed .mov but a significantly smaller file. Else you’ll end up uncompressed .mov
 

The workflow of the program is as follows.

________________________________________________________________________________________________________________________________________ 
Customization
________________________________________________________________________________________________________________________________________

If you would like to change the mediaconch file to check for different properties of the files simply open up the .xml files that shipped along with the program in the GUI version of Mediaconch to change the necessary values. The name of the file must remain the same.  These files must reside in the “Tools” folder configured above. 

Mezzanine-policy.xml – Policy for mezzanine file.

mov-policy.xml – Policy for .mov master file.

FFV1-policy.xml – Policy for .mkv FFV1 master file.

