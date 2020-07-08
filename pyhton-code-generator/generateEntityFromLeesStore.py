
############### PYTHON SCRIPT TO GENERATE NEW ENTITIES FROM LEE'S CODE -- HOW-TO ###########################################################
# Thanks goes to Lee Richardson for the video tutorial and the source code for how to create a working generic entity in ASP.NET Boilerplate
# This Python code copies the Product entity, its AppService, Angular components and html files from Lee's code and generates a new entity 
# with a new name defined by the user. But it is generic enough that it can use other people's code as source as well.
#
# PLEASE FEEL FREE TO IMPROVE THIS SCRIPT AND SHARE
#
# This works fine with Python 3. Save this into a generateEntityFromLeesStore.py file
#
# If you want to create a new entity named Fig with its Angular components, run:
#            python generateEntityFromLeesStore.pl Fig Figs
# If you want to create a new entity named Shelf with its Angular components, run:
#            python generateEntityFromLeesStore.pl Shelf Shelves
# If you like, you can also python generateEntityFromLeesStore.pl Shelf Shelfs
#
# You need to enter the USER INPUT below
#
# target_dir is the root directory of your code that (i) you want to create a new entity in, and (ii) it contains the angular and aspnet-core folders
# solutionName is the name space of your solution. This will replace the name space in files in the source code.
#
# source_dir is the root directory that (i) you want to copy an ready-to-use entity from, and (ii) it contains the angular and aspnet-core folders
#
# Entity is the name of the ready-to-use entity in the source code, It must start with capital letter
# Entities is the plural form of the Entity in the source code
#
# Example:
# Suppose you save your project in the "C:\MyProjects\" folder, and you have created an ASP.NET Boiler plate with the name of "Sample" and unzipped the file
# in the "C:\MyProjects\" folder, and the "C:\MyProjects\Sample\5.6.0\" is the folder that contains the angular and aspnet-core folders. Then set
#                     target_dir = "C:\\MyProjects\\Sample\\5.6.0\\" 
#                                     *** notice double dashes *** 
# The boiler plate sets the name space as "Sample" by default unless you change it. So set
#                     solutionName = "Sample"
#
# Suppose you downloaded and unziped Lee Richardson's LeesStore app at https://github.com/lprichar/LeesStore/pull/13 in your "C:\temp\" and
# C:temp\LeesStore-product-crud\ is the folder that has the angular and aspnet-core folders. Then set
#                     source_dir = "C:\\temp\\LeesStore-product-crud\\
#                                     *** notice double dashes *** 
# Check the name space in the source code. In this example, it is "LeesStore", so
#                     sourceSsolutionName = "LeesStore"
#
# Suppose you want to generate your new Fig entity by using Lee's Product entity. Then set
#                                  Entity = "Product"
#                                  Entities = "Products"
#
# Change the following connection strings according to your settings
# the following is the default in ABP
#                                  originalConnectionString = "Server=localhost; Database=" + solutionName+ "Db; Trusted_Connection=True;"
# and this is your new connection string
#                                  myConnectionString = "<your connection string here>"
# In case you want to change part of it as I do here:
#                                  originalConnectionString = "Server=localhost"
#                                  myConnectionString = "Data Source=(localdb)\\\\MSSQLLocalDB"
#                                     *** notice "double" double dashes *** 
#
#
# If you want to group your new entities in a new Models folder under the Core folder, set
#                                  groupEntities = True
#
# Run the following command in the folder of this file:
#                                  python generateEntityFromLeesStore.pl Fig Figs
#
#
# Add migrations, update database
#
# Add properties to your new Fig entity, update the html files in folder C:\MyProjects\Sample\angular\src\app\figs and in its subfolders
# with properties of your new Fig entity
#
# Run nswag
#
###################################################################################################################################


############  USER INPUT ###########
# the root directory of your app that contains the angular and aspnet-core folders
target_dir = "..\\"

# the namespace of your solution
solutionName = "LeesStore"

# the root directory of the app, from which you will copy a ready-to0use entity, that contains the angular and aspnet-core folders
source_dir = "..\\"

# the name space of the app you will copy from
sourceSolutionName = "LeesStore"

# the name of the Entity in the source app that you will copy
Entity = "Product"
Entities = "Products"

# Change the following connection strings according to your settings
# the following is the default
originalConnectionString = "Server=localhost; Database=" + solutionName+ "Db; Trusted_Connection=True;"
myConnectionString = "<your connection string here>"
# In case you want to change part of it as I do here, you can delete the override below:
originalConnectionString = "Server=localhost"
myConnectionString = "Data Source=(localdb)\\\\MSSQLLocalDB"

# set the following to True if you want to group all new entities in a Models folder and False if you not want grouping
groupEntities = False

# OPTIONAL: To add table to database, permissions, item to nav bar etc, set the following to True, and add the following lines
# to your corresponsing files
generateRemaining = True
# If you are using Angular 9xxx, set the following to true
correctForNg9 = True


##### Marker in files after which new lines will be added
addPermissionName = "public const string Pages_Roles = \"Pages.Roles\";"
registerPermission = "context.CreatePermission(PermissionNames.Pages_Tenants, L(\"Tenants\"), multiTenancySides: MultiTenancySides.Host);"
addLocalization ="<texts>"
addDBSet = "/* Define a DbSet for each entity of the application */"
addDeclarations = "declarations: ["
addProviders = "entryComponents: ["
addModules = "import { SidebarMenuComponent } from './layout/sidebar-menu.component';"
addProxies = "providers: ["
addMenuItem ="new MenuItem(this.l('HomePage'), '/app/home', 'fas fa-home'),"
addRoute = "children: ["
addModuleToRoute = "import { ChangePasswordComponent } from './users/change-password/change-password.component';"

##### Material modules and components

importMaterialModules = [ "import { MatMenuModule } from '@angular/material/menu';\n",   
    "import { MatFormFieldModule } from '@angular/material/form-field';\n",   
    "import { MatDialogModule } from '@angular/material/dialog';\n",
    "import { MatIconModule } from '@angular/material/icon';\n" ]

addMaterialModules = [ "    MatMenuModule,\n",
    "    MatFormFieldModule,\n",
    "    MatDialogModule,\n",
    "    MatIconModule,\n" ]   


#########################

Models = ""
if groupEntities:
    Models = "Models"

entity = Entity.lower()
entities = Entities.lower()

import os
import sys
from distutils.dir_util import copy_tree
import shutil
import time


# Function to change file names and directory names
def rename_files(newName,directory):
    for dir, subdirs, names in os.walk( directory ):
        for name in names:
            src = os.path.join( dir, name )
            new_filename = name
            newname = newName.lower()
            
            if Entity in name:
                new_filename = new_filename.replace(Entity,newName)
            else:          
                new_filename = new_filename.replace(entity,newname)
                
            dst = os.path.join( dir, new_filename) 
            os.rename(src, dst)
        
        new_dir = dir
        if Entity in dir:
            new_dir = new_dir.replace(Entity,newName)
        else:          
            new_dir = new_dir.replace(entity,newname)

            os.rename(dir, new_dir)
        

# Function to replace strings in files
def recursive_replace( root, pattern, replace ):
    for dir, subdirs, names in os.walk( root ):
        for name in names:
            path = os.path.join( dir, name )
            text = open( path ).read()
            if pattern in text:
                open( path, 'w' ).write( text.replace( pattern, replace ) )
                
# Function to add lines into files
def add_line(path,marker,line,emptyspace):                
    text = open( path ).read()
    if marker in text:
        if line not in text:
            replace = marker + "\n" + emptyspace + line
            open( path, 'w' ).write( text.replace( marker, replace ) )


######### ASP.NET CORE #########

New_model = sys.argv[1]
new_model = New_model.lower()
New_models = sys.argv[2]
new_models = New_models.lower()
newNameSpace = solutionName
oldNameSpace = sourceSolutionName

#### MODEL ####
# Check/Create Models directory
toDirectory = target_dir + "aspnet-core\\src\\" + solutionName + ".Core\\" + Models
if not os.path.exists(toDirectory):
	os.mkdir(toDirectory)
	
    
# Check/Create new Entity directory
toDirectory = toDirectory + "\\" + New_models
if os.path.exists(toDirectory):
    shutil.rmtree(toDirectory, ignore_errors=False, onerror=None)
os.mkdir(toDirectory)
	
    
# Copy Model
fromDirectory = source_dir + "aspnet-core\\src\\" + sourceSolutionName + ".Core\\" + Entities
copy_tree(fromDirectory, toDirectory)


# rename files
rename_files(New_model,toDirectory)

# replace model name
recursive_replace(toDirectory,Entity,New_model)
recursive_replace(toDirectory,entity,new_model)
recursive_replace(toDirectory,oldNameSpace,newNameSpace)

#### APP SERVICES ####
toDirectory = target_dir + "aspnet-core\\src\\" + solutionName + ".Application"
	    
# Check/Create new AppService directory
toDirectory = toDirectory + "\\" + New_models
if os.path.exists(toDirectory):
    shutil.rmtree(toDirectory, ignore_errors=False, onerror=None)
os.mkdir(toDirectory)
	
 
# Copy App Services
fromDirectory = source_dir + "aspnet-core\\src\\" + sourceSolutionName + ".Application\\" + Entities
copy_tree(fromDirectory, toDirectory)


# rename files
rename_files(New_model,toDirectory)

# replace model name
recursive_replace(toDirectory,Entity,New_model)
recursive_replace(toDirectory,entity,new_model)
recursive_replace(toDirectory,oldNameSpace,newNameSpace)


######### ANGULAR #########


toDirectory = target_dir + "angular\\src\\app\\" + new_models
	    
# Check/Create new component directory
if not os.path.exists(toDirectory):
	os.mkdir(toDirectory)
	
 
# Copy components etc
fromDirectory = source_dir + "angular\\src\\app\\" + entities
copy_tree(fromDirectory, toDirectory)


# replace model name
recursive_replace(toDirectory,Entity,New_model)
recursive_replace(toDirectory,entity,new_model)

if generateRemaining:
    # Add new permission name
    permissionsDirectory = target_dir + "aspnet-core\\src\\" + solutionName + ".Core\\Authorization"
    path = os.path.join( permissionsDirectory, "PermissionNames.cs" )
    marker = addPermissionName
    addLine = "public const string Pages_" + New_models + " = \"Pages." + New_models + "\";"
    emptyspace = "\n        "
    add_line(path,marker,addLine,emptyspace)
    
    
    # Register new permission
    path = os.path.join( permissionsDirectory, solutionName + "AuthorizationProvider.cs" )
    marker = registerPermission
    addLine = "context.CreatePermission(PermissionNames.Pages_" + New_models + ", L(\"" + New_models + "\"));"
    emptyspace = "            "
    add_line(path,marker,addLine,emptyspace)
    
            
    # Add localization
    localizationDirectory = target_dir + "aspnet-core\\src\\" + solutionName + ".Core\\Localization\\SourceFiles"
    path = os.path.join( localizationDirectory, solutionName + ".xml" )
    marker = addLocalization
    addLine = "<text name=\"" + New_models + "\" value=\"" + New_models + "\" />"
    emptyspace = "\n    "
    add_line(path,marker,addLine,emptyspace)
    
    # Add to database
    dbDirectory = target_dir + "aspnet-core\\src\\" + solutionName + ".EntityFrameworkCore\\EntityFrameworkCore"
    path = os.path.join( dbDirectory, solutionName + "DbContext.cs" )
    marker = addDBSet
    addLine = "public DbSet<" + New_model + "> " + New_models + " { get; set; }"
    emptyspace = "\n        "
    add_line(path,marker,addLine,emptyspace)
    marker = "using " + solutionName + ".MultiTenancy;"
    addLine = "using " + solutionName + "." + New_models + ";"
    emptyspace = ""
    add_line(path,marker,addLine,emptyspace)
    
    
    # Add modules in angular
    ngDirectory = target_dir + "angular\\src\\app"
    path = os.path.join( ngDirectory, "app.module.ts" )
    marker = addModules
    addLine = "// " + new_models + "\n"
    addLine = addLine + "import { " + New_models + "Component } from '@app/" + new_models + "/" + new_models + ".component';\n"
    addLine = addLine + "import { Create" + New_model + "DialogComponent } from './" + new_models + "/create-" + new_model + "/create-" + new_model + "-dialog.component';\n"
    addLine = addLine + "import { Edit" + New_model + "DialogComponent } from './" + new_models + "/edit-" + new_model + "/edit-" + new_model + "-dialog.component';\n"   
    emptyspace = ""
    add_line(path,marker,addLine,emptyspace)
    
    # Add Material modules
    addLine = "// material modules \n"
    emptyspace = ""
    add_line(path,marker,addLine,emptyspace)
    
    for addLine in importMaterialModules:
        add_line(path,marker,addLine,emptyspace)
    
    marker = "imports: ["
    addLine = "    MatMenuModule,\n"
    add_line(path,marker,addLine,emptyspace)
    
    for addLine in addMaterialModules:
        add_line(path,marker,addLine,emptyspace)
 
        
    # Register modules in angular    
    marker = addDeclarations
    addLine = "    // " + new_models + "\n    " + New_models +"Component,\n    Create"+ New_model + "DialogComponent,\n    Edit" + New_model + "DialogComponent,"
    emptyspace = ""
    add_line(path,marker,addLine,emptyspace)
    
    # Register providers in angular
    marker = addProviders
    addLine = "    // " + new_models + "\n    Create"+ New_model + "DialogComponent,\n    Edit" + New_model + "DialogComponent,"
    emptyspace = ""
    add_line(path,marker,addLine,emptyspace)   

    # Register route
    path = os.path.join( ngDirectory, "app-routing.module.ts" )
    marker = addRoute
    addLine = "                    { path: '" + new_models + "', component: " + New_models + "Component, data: { permission: 'Pages." + New_models + "' }, canActivate: [AppRouteGuard] },"
    emptyspace = ""
    add_line(path,marker,addLine,emptyspace)
    marker = addModuleToRoute
    addLine = "import { " + New_models + "Component } from 'app/" + new_models + "/" + new_models + ".component';"
    emptyspace = ""
    add_line(path,marker,addLine,emptyspace)


    
    # Register service proxy in angular
    ngDirectory = target_dir + "angular\\src\\shared\\service-proxies"
    path = os.path.join( ngDirectory, "service-proxy.module.ts" )
    marker = addProxies
    addLine = "        ApiServiceProxies." + New_models + "ServiceProxy,"
    emptyspace = ""
    add_line(path,marker,addLine,emptyspace)
    
    # Add to menu on the sidebar
    ngDirectory = target_dir + "angular\\src\\app\\layout"
    path = os.path.join( ngDirectory, "sidebar-nav.component.ts" )
    marker = addMenuItem
    addLine = "      new MenuItem(this.l('" + New_models + "'),'/app/" + new_models + "','fas fa-building','Pages." + New_models + "'),"
    emptyspace = ""
    add_line(path,marker,addLine,emptyspace)
    
    # Finally set your connection string here  
    dbDirectory = target_dir + "aspnet-core\\src\\" + solutionName + ".Web.Host"
    path = os.path.join( dbDirectory, "appsettings.json" )
    text = open( path ).read()
    open( path, 'w' ).write( text.replace( originalConnectionString, myConnectionString ) )
    

    
    
            
            

