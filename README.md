# Online_School_Teacher

There are Three Types of Actors into this project,

1)Admin
2)Teacher
3)Student

# Already Created

When Teacher will register into the system a JWT_Token will initally will be null.
However when admin will approve the teacher registration then a JWT_Token will be
provided to the teacher. The Token will stay valid for 2months. The Token will 
saved to the database. By Every Login of specific teacher the token will be verify
and return true if the Token is valid else return false; 

Admin Can Block a Teacher and Renew the JWT_Token of Teachers.
Admin Can Create a Courses.

# Future Concept

Teacher can take a courses provided by the Admin and can add tutorial under a specific
Course. Teacher will able to provide video files / tutorials into the system under a specific
Course.

Student can watch the Tutorials provided by the Teachers.

Finally Using The ML.NET There two machine learning cocept will be implemented into this project

1)Sentiment Analysis (To Get The Best Possible Search Result For Student)

2)Image Processing (To Get is the teacher is uploading proper videos).

#Objective

Because this project will be built in Web API and ASP.NET Core MVC and there will be a access
JWT Token for every user this project can be implemented in any platfrom using API concept.

