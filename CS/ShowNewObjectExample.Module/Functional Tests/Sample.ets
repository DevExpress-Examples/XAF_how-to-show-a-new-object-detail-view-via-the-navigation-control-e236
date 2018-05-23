;#DropDB ShowNewObjectExampleEasyTest

#Application ShowNewObjectExampleWin
#Application ShowNewObjectExampleWeb

*Action Navigation(User)

*ProcessRecord
 Full Name = ''

#IfDef ShowNewObjectExampleWeb
*Action Edit
#EndIf
 
*FillForm
 Full Name = Test User
 
*Action Cancel

#IfDef ShowNewObjectExampleWin
*HandleDialog
 Respond = Yes
#EndIf

*CheckFieldValues
 Full Name = ''
