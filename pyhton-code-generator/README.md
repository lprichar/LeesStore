# GenerateABPentityFromLeesStore
 PYTHON SCRIPTS TO GENERATE NEW ENTITIES FROM LEE'S CODE -- HOW-TO 

 [ html files are not working yet, probably a version issue, otherwise all the files are being generated, everything is being registered ]
 
 [ a correction to the code by (easily) better programmers would be much appreciated ]

 Things to do:

	1. unzip LeesStore app
	2. Unzip your app
	3. Suppose you want to generate a new entity Fig
	4. run "python generateEntityFromLeesStore.py Fig Figs (read below for setting up this)
	5. go to the angular folder in your app, run the following (you need to do this once)
		npm install
		npm install --save @angular/material
		npm install --save @angular/cdk

	6. open your solution in visual studio
	7. set web.host as start-up project (if you have not done already)
	8. change to entityframework in your package manager, run
		add-migration
		update-database
	9. run the api
	10. go to angular/nswag folder, run refresh.bat
	11. if you are using Angular 9xxx, run
		python correctForNg9.py Fig Figs (you need to enter your target director in this py file as well)
	12. run ng serve in the angular directory

 Thanks goes to Lee Richardson for the video tutorial and the source code for how to create a working generic entity in ASP.NET Boilerplate, and also improving/correcting numerous parts in this Python code.
 This Python code copies the Product entity, its AppService, Angular components and html files from Lee's code and generates a new entity 
 with a new name defined by the user, it registers the permissions, components, proxy services etc. 
 
 Lee's video tutorial: https://www.youtube.com/watch?v=xmHTYF5RvMs
 Source code: https://github.com/lprichar/LeesStore/pull/13
 
 PLEASE FEEL FREE TO IMPROVE THIS SCRIPT AND SHARE. There are many opportunities to automate the code generation. It would be greatly appreciate if you could make and share your improvements here.
 
 This works fine with Python 3. Save this into a generateEntityFromLeesStore.py file
 
 If you want to create a new entity named Fig with its Angular components, run:
            
            python generateEntityFromLeesStore.py Fig Figs

 If you want to create a new entity named Shelf with its Angular components, run:

            python generateEntityFromLeesStore.py Shelf Shelves
 If you like, you can also python generateEntityFromLeesStore.py Shelf Shelfs
 
 You need to enter the USER INPUT below
 
 target_dir is the root directory of your code that (i) you want to create a new entity in, and (ii) it contains the angular and aspnet-core folders
 solutionName is the name space of your solution. This will replace the name space in files in the source code.
 
 source_dir is the root directory that (i) you want to copy an ready-to-use entity from, and (ii) it contains the angular and aspnet-core folders
 
 Entity is the name of the ready-to-use entity in the source code, It must start with capital letter
 Entities is the plural form of the Entity in the source code
 
 Example:
 Suppose you save your project in the "C:\MyProjects\" folder, and you have created an ASP.NET Boiler plate with the name of "Sample" and unzipped the file
 in the "C:\MyProjects\" folder, and the "C:\MyProjects\Sample\5.6.0\" is the folder that contains the angular and aspnet-core folders. Then set

                     target_dir = "C:\\MyProjects\\Sample\\5.6.0\\" 

                                     *** notice double dashes *** 

 The boiler plate sets the name space as "Sample" by default unless you change it. So set

                     solutionName = "Sample"
 
 Suppose you downloaded and unziped Lee Richardson's LeesStore app at https://github.com/lprichar/LeesStore/pull/13 in your "C:\temp\" and
 C:temp\LeesStore-product-crud\ is the folder that has the angular and aspnet-core folders. Then set

                     source_dir = "C:\\temp\\LeesStore-product-crud\\

                                     *** notice double dashes *** 

 Check the name space in the source code. In this example, it is "LeesStore", so

                     sourceSolutionName = "LeesStore"
 
 Suppose you want to generate your new Fig entity by using Lee's Product entity. Then set

                                  Entity = "Product"

                                  Entities = "Products"
 
 Change the following connection strings according to your settings the following is the default in ABP

                                  originalConnectionString = "Server=localhost; Database=" + solutionName+ "Db; Trusted_Connection=True;"

 and this is your new connection string

                                  myConnectionString = "<your connection string here>"

 In case you want to change part of it as I do here:

                                  originalConnectionString = "Server=localhost"

                                  myConnectionString = "Data Source=(localdb)\\\\MSSQLLocalDB"

                                     *** notice "double" double dashes *** 
 
 
 If you want to group your new entities in a new Models folder under the Core folder, set

                                  groupEntities = True
 
 Run the following command in the folder of this file:

                                  python generateEntityFromLeesStore.py Fig Figs
 
 
 Add migrations, update database
 
 Add properties to your new Fig entity, update the html files in folder C:\MyProjects\Sample\angular\src\app\figs and in its subfolders
 with properties of your new Fig entity
 
 Run nswag
 
