#Application ShowNewObjectExample.Win
#Application ShowNewObjectExample.Web

*Action Log On
*Action Navigation(Issues.Create New...)
;*Action New
*FillForm
 Subject = New Task
*Action Save and Close
*Action Navigation(Issues.Inbox)
*CheckTable Demo Issue
 Columns = Subject
 Row = New Task
