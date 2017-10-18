Module miscModule
    Public UserName As String
    Public Sub LoadForm(ByVal f As Form, ByVal parentForm As Form, ByVal formCaption As String)
        f.MdiParent = parentForm
        f.Text = formCaption
        f.Show()
    End Sub
    Public formName As String
    'This function controls the buttons on the forms. 
    Public Sub ButtonControl(ByVal ActionName As String, ByVal cmdAdd As Button, ByVal cmdModify As Button, ByVal cmdSave As Button, ByVal cmdDelete As Button, ByVal cmdCancel As Button, ByVal cmdFind As Button)
        If ActionName = "cmdAdd" Then
            cmdAdd.Enabled = False
            cmdModify.Enabled = False
            cmdDelete.Enabled = False
            cmdFind.Enabled = False
            cmdCancel.Enabled = True
            cmdSave.Enabled = True
        End If
        If ActionName = "cmdModify" Then
            cmdAdd.Enabled = False
            cmdModify.Enabled = False
            cmdDelete.Enabled = False
            cmdFind.Enabled = False
            cmdCancel.Enabled = True
            cmdSave.Enabled = True
        End If
        If ActionName = "cmdSave" Then
            cmdAdd.Enabled = True
            cmdModify.Enabled = False
            cmdDelete.Enabled = False
            cmdFind.Enabled = True
            cmdCancel.Enabled = False
            cmdSave.Enabled = False
        End If
        If ActionName = "cmdDelete" Then
            cmdAdd.Enabled = True
            cmdModify.Enabled = False
            cmdDelete.Enabled = False
            cmdFind.Enabled = True
            cmdCancel.Enabled = False
            cmdSave.Enabled = False
        End If
        If ActionName = "cmdCancel" Then
            cmdAdd.Enabled = True
            cmdModify.Enabled = False
            cmdDelete.Enabled = False
            cmdFind.Enabled = True
            cmdCancel.Enabled = False
            cmdSave.Enabled = False
        End If
        If ActionName = "cmdFind" Then
            cmdAdd.Enabled = True
            cmdModify.Enabled = True
            cmdDelete.Enabled = True
            cmdFind.Enabled = True
            cmdCancel.Enabled = False
            cmdSave.Enabled = False
        End If
        If ActionName = "" Then
            cmdAdd.Enabled = True
            cmdModify.Enabled = False
            cmdDelete.Enabled = False
            cmdFind.Enabled = True
            cmdCancel.Enabled = False
            cmdSave.Enabled = False
        End If
    End Sub

    'This function will be used to disable and enable fields on form
    Public Sub EnableDisableControlsOnForm(ByVal g As GroupBox, ByVal x As Integer)
        Dim obj As Control
        'x=0 to disable controls and x=1 to enable controls and option=2 is to clear fields
        If (x = 0) Then
            For Each obj In g.Controls
                If (TypeOf obj Is TextBox Or TypeOf obj Is ComboBox Or TypeOf obj Is DataGridView Or TypeOf obj Is MaskedTextBox Or TypeOf obj Is DateTimePicker) Then
                    obj.Enabled = False
                End If
            Next
        End If
        If (x = 1) Then
            For Each obj In g.Controls
                If (TypeOf obj Is TextBox Or TypeOf obj Is ComboBox Or TypeOf obj Is DataGridView Or TypeOf obj Is MaskedTextBox Or TypeOf obj Is DateTimePicker) Then
                    obj.Enabled = True
                End If
            Next
        End If
        If (x = 2) Then
            For Each obj In g.Controls
                If (TypeOf obj Is TextBox) Then
                    Dim t As TextBox = obj
                    t.Text = ""
                End If
                If TypeOf obj Is ComboBox Then
                    Dim cb As ComboBox = obj
                    cb.Items.Clear()
                    cb.Text = ""
                End If
                If TypeOf obj Is DataGridView Then
                    Dim dv As DataGridView = obj
                    dv.DataSource = vbNull
                End If
            Next
        End If
    End Sub
    'This function will scan for a single quote in the input string. If found then it will append two quotes
    'in place of the single quote so that the item string containing the single quote can be easily parsed in
    'the SQL query
    Public Function ScanStringForQuote(ByVal input As String) As String
        Dim str As String = input
        Dim c As Char
        Dim tmpStr As String = ""
        For Each c In str
            If c = "'" Then
                tmpStr = tmpStr + "''"
            Else
                tmpStr = tmpStr + c
            End If
        Next
        ScanStringForQuote = tmpStr
    End Function
End Module
