# userDemo1

To setup this repository on your local machine follow the steps:
  1- Copy / Clone repository to your disk drive.
  2- find the script folder for 'UserDemoDBScript.sql' file and run it on you your local SQL server to create database.
  3- find the 'UserDemo_AdminInsert.sql' file from script folder and run it to generate first administrator account, with out it you will not be able to login.
  4- Note: remeber to correct connection string of 'dbModel.edmx' file as per your computer/SQL server name.
  5- open the folder where you copied this repository, find 'userDemo1.sln' file and double click on it to open with visual studio 2019.
  6- build the solution once to check and confirm there is no error.
  7- Run application:
        
        a) UserId : admin@userdemo.com    &     Password : Test@123
        b) to test this application click on Roles and Add some Roles first, like Manager,Supervisor and Users etc.
        c) when you have finished Roles, now see at right side of every Roles record, there is 3 Icons (Edit, Permissions, and delete)
        d) click the middle one (permission) to setup the permissions for the roles.
        e) once you have setup all permissions for every roles, now you can move to create new user, click on 'Users' link on Top Menu.
        f) only one Administrator user will appear in list.
        g) click on create new link at top of list. adn fill the data form and hit submit. new user is created.
        h) now click on 'Pencil' Icon to edit the user.
        i) on Edit screen now at right side of screen Roles will appears with tags "Roles Assigned" and "Roles Not Assigned"
        j) click on Add Or Remove Button to assign the role to the user.
        K) now logout and login with newly created user and check on customer, Operations, User link, based on Roles you assigned with permissions GUI will reflect.
        
        
This Application is developed in .NET Framework 4.7.2 and EntityFrameWork Core 6X.
For Security and Users Management we are not using any Microsft built in technology, instad we built our own customize security frame work and it is now just only for demo purpose
In database Password is stored in plain Text Later on we can apply Data Encryption to secure the password.

For any Further Qustions or details please contact me!
Naved Ahmed.
