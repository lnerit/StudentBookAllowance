Imports System.Data.OleDb

Public Class frmLogin
    Dim dbQuery As New dbQuery
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load


    End Sub

    Private Sub cmdCancel_Click(sender As Object, e As EventArgs) Handles cmdCancel.Click
        Me.Close()
    End Sub

    Private Sub cmdLogin_Click(sender As Object, e As EventArgs) Handles cmdLogin.Click
        If txtUserId.Text = "" Then
            MessageBox.Show("Please enter your User Name", "Missing User Name", MessageBoxButtons.OK, MessageBoxIcon.Stop)
            Exit Sub
        End If
        'Validate login credentials
        If (dbQuery.ValidateLogin(txtUserId.Text, txtPassword.Text)) Then
            'Load the parent form
            UserName = txtUserId.Text
            Dim mdi As New mdiMain
            mdi.Show()
            Me.Hide()

            'Reset the fields after login 
            Me.txtUserId.Text = ""
            Me.txtPassword.Text = ""
        Else
            MessageBox.Show("Please check you login credentials and retry!", "Invalid Login", MessageBoxButtons.OK, MessageBoxIcon.Stop)
        End If
    End Sub
End Class
