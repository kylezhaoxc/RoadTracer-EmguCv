# RoadTracer
Trace the edge of the road in a hallway, and mark out the center line of the road

This is a module of an auto-patrol robot designed to go around a certain route in the hallway.
There are ultrasonic sensors to ensure the robot keeping a certain distance to the wall, 
This module is a possiblereplacement to ultrasonic.

This program works with EmguCv and Csharp, using methods like canny, hough lines, and applied some perceptual theories.
I hope this program to be robust, and accurate.

For an incoming frame, the contour will be extracted with canny method, and several HoughLines will be calculated.
After filtering unreasonable lines, the rest of the lines will be preserved and calculate the vanishing point of the image.
The bisector of the angle formed by the vanishing point and the edge of the two sides of the hallway will be the center line.

![image](https://github.com/kylezhaoxc/RoadTracer-EmguCv/Screenshots/1.jpg)
![image](https://github.com/kylezhaoxc/RoadTracer-EmguCv/Screenshots/1-canny.jpg)
![image](https://github.com/kylezhaoxc/RoadTracer-EmguCv/Screenshots/1-HoughLines.jpg)
![image](https://github.com/kylezhaoxc/RoadTracer-EmguCv/Screenshots/1-lines.jpg)

![image](https://github.com/kylezhaoxc/RoadTracer-EmguCv/Screenshots/2.jpg)
![image](https://github.com/kylezhaoxc/RoadTracer-EmguCv/Screenshots/2-canny.jpg)
![image](https://github.com/kylezhaoxc/RoadTracer-EmguCv/Screenshots/2-HoughLines.jpg)
![image](https://github.com/kylezhaoxc/RoadTracer-EmguCv/Screenshots/2-lines.jpg)

![image](https://github.com/kylezhaoxc/RoadTracer-EmguCv/Screenshots/3.jpg)
![image](https://github.com/kylezhaoxc/RoadTracer-EmguCv/Screenshots/3-canny.jpg)
![image](https://github.com/kylezhaoxc/RoadTracer-EmguCv/Screenshots/3-HoughLines.jpg)
![image](https://github.com/kylezhaoxc/RoadTracer-EmguCv/Screenshots/3-lines.jpg)

![image](https://github.com/kylezhaoxc/RoadTracer-EmguCv/Screenshots/4.jpg)
![image](https://github.com/kylezhaoxc/RoadTracer-EmguCv/Screenshots/4-canny.jpg)
![image](https://github.com/kylezhaoxc/RoadTracer-EmguCv/Screenshots/4-HoughLines.jpg)
![image](https://github.com/kylezhaoxc/RoadTracer-EmguCv/Screenshots/4-lines.jpg)