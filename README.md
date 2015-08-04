# RoadTracer with EmguCV
##Briefing
This is a CV module used to tell whether the current camera position is on the center line of the road or hallway. The program is written in C# with EmguCV.
##Structure
####*RoadTracer
this is a class library project, which generates RoadTracer.dll. The project contains these:<br>
######->LineHelper.cs
Wrapped some tool functions like calculating the distance to a line, and find the coordinate of the intersection of two lines.
######->Operator.cs
Kernel logical process of finding proper lines and calculating bisectors.
######->TextException.cs
My Own exception type, maybe useful in the future?
####*TestConsole
A console application to test static images. Exceptions will be shown on the terminal window and break the process.
####*TestWpf
A Wpf Application to test video stream, any exceptions will stop current process and go on with next frame. Except for MainWindow.xaml, App.xaml and some basic things, I added this:
######->UIHandler.cs
To display or change the image shown on the form is sooooo complicated in wpf, especially with Emgu.CV.Image<>.
Then I wrapped this in the class.
##User Tips
RoadTracer is a class library, so you could build this project once, find the RoadTracer.dll in its /bin directory, and use that as a reference in your own project.

This works with emgucv-windows-universal-cuda 2.4.10.1940.

Remember: Applications could only run when you copy  this folder:
/install_position_of_EmguCV/bin/x86
to 
/Your_application/bin/Debug/

You can also find BetterTogether.dll in TestWpf. which is used for testing the application with a Windows Phone running an unique application.
Feel free to use a web cam instead.
Remember to change the Width and Height parameters in RoadTracer::Operator.cs
##How it works?
For an incoming frame, like this:
![image](https://github.com/kylezhaoxc/RoadTracer-EmguCv/raw/master/Screenshots/1.jpg)<br>
First,the contour will be extracted with canny method.
Considering the upper part of the images contains few things about the road, I took the lower part for ROI.
![image](https://github.com/kylezhaoxc/RoadTracer-EmguCv/raw/master/Screenshots/1-canny.jpg)<br>
Then , based on canny contours, several HoughLines will be calculated.
![image](https://github.com/kylezhaoxc/RoadTracer-EmguCv/raw/master/Screenshots/1-HoughLines.jpg)<br>
After filtering unreasonable lines, the rest of the lines will be preserved and calculate the vanishing point of the image.
The bisector of the angle formed by the vanishing point and the edge of the two sides of the hallway will be the center line.
![image](https://github.com/kylezhaoxc/RoadTracer-EmguCv/raw/master/Screenshots/1-lines.jpg)
<br>

I will add some other examples :
![image](https://github.com/kylezhaoxc/RoadTracer-EmguCv/raw/master/Screenshots/2.jpg)
![image](https://github.com/kylezhaoxc/RoadTracer-EmguCv/raw/master/Screenshots/2-canny.jpg)
![image](https://github.com/kylezhaoxc/RoadTracer-EmguCv/raw/master/Screenshots/2-HoughLines.jpg)
![image](https://github.com/kylezhaoxc/RoadTracer-EmguCv/raw/master/Screenshots/2-lines.jpg)

![image](https://github.com/kylezhaoxc/RoadTracer-EmguCv/raw/master/Screenshots/3.jpg)
![image](https://github.com/kylezhaoxc/RoadTracer-EmguCv/raw/master/Screenshots/3-canny.jpg)
![image](https://github.com/kylezhaoxc/RoadTracer-EmguCv/raw/master/Screenshots/3-HoughLines.jpg)
![image](https://github.com/kylezhaoxc/RoadTracer-EmguCv/raw/master/Screenshots/3-lines.jpg)

![image](https://github.com/kylezhaoxc/RoadTracer-EmguCv/raw/master/Screenshots/4.jpg)
![image](https://github.com/kylezhaoxc/RoadTracer-EmguCv/raw/master/Screenshots/4-canny.jpg)
![image](https://github.com/kylezhaoxc/RoadTracer-EmguCv/raw/master/Screenshots/4-HoughLines.jpg)
![image](https://github.com/kylezhaoxc/RoadTracer-EmguCv/raw/master/Screenshots/4-lines.jpg)

Hope it helps!
