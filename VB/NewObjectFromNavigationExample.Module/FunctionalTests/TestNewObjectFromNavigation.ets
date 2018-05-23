#Application NewObjectFromNavigationExample.Win
#Application NewObjectFromNavigationExample.Win.MDI
#Application NewObjectFromNavigationExample.Web

*Action Navigation(Issue)

*Action Navigation(Create New Issue...)

*FillForm
 Subject = Test Issue

*Action Save

*Action Navigation(Issue)

*CheckTable Issue
 Columns = Subject
 Row = Test Issue

;Ensure that the Issue can be created when all MDI tabs are closed:
#IfDef NewObjectFromNavigationExample.Win.MDI

*Action Close

*Action Navigation(Create New Issue...)

*FillForm
 Subject = Test Issue 2

#EndIf