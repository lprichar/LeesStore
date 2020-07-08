import os
import sys
from distutils.dir_util import copy_tree
import shutil

############  USER INPUT ###########
# the root directory of your app that contains the angular and aspnet-core folders
target_dir = "C:\\MyProjects\\Sample\\5.6.0\\"
#################################################################

New_model = sys.argv[1]
new_model = New_model.lower()
New_models = sys.argv[2]
new_models = New_models.lower()

ngDirectory = target_dir + "angular\\src\\app\\" + new_models
path = os.path.join( ngDirectory, new_models+".component.ts" )
oldLine = "import { MatDialog } from '@angular/material';"
newLine = "import { MatDialog } from '@angular/material/dialog';"
text = open( path ).read()
open( path, 'w' ).write( text.replace( oldLine, newLine ) )

ngDirectory = target_dir + "angular\\src\\app\\" + new_models + "\\create-" + new_model
path = os.path.join( ngDirectory, "create-"+ new_model + "-dialog.component.ts" )
oldLine = "import { MatDialogRef } from '@angular/material';"
newLine = "import { MatDialogRef } from '@angular/material/dialog';"
text = open( path ).read()
open( path, 'w' ).write( text.replace( oldLine, newLine ) )

ngDirectory = target_dir + "angular\\src\\app\\" + new_models + "\\edit-" + new_model
path = os.path.join( ngDirectory, "edit-"+ new_model + "-dialog.component.ts" )
oldLine = "import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';"
newLine = "import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';"
text = open( path ).read()
open( path, 'w' ).write( text.replace( oldLine, newLine ) )
