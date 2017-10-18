Imports System.Data.OleDb

Public Class dbConnection
    Dim cnn As OleDbConnection = New OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=Student.accdb;Persist Security Info=False")
    Public Function getConnection() As OleDbConnection
        Try
            cnn.Open()
            getConnection = cnn
        Catch
            cnn.Close()
            MessageBox.Show(Err.GetException.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop)
        End Try

    End Function
End Class
