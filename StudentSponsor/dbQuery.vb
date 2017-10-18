Imports System.Data.OleDb

Public Class dbQuery
    Dim connection As New dbConnection()
    Dim conn As New OleDbConnection
    Public Sub ExcecuteNonQuery(ByVal sqlString As String)
        conn = connection.getConnection()
        Dim cmd As OleDbCommand = New OleDbCommand(sqlString, conn)
        Using (cmd)
            cmd.ExecuteNonQuery()
        End Using
        If Not (IsDBNull(conn)) Then
            conn.Close()
        End If
    End Sub

    Public Function ExcecuteScalarQuery(ByVal sqlString As String) As String
        Try
            conn = connection.getConnection()
            Dim cmd As OleDbCommand = New OleDbCommand(sqlString, conn)
            Using (cmd)
                ExcecuteScalarQuery = cmd.ExecuteScalar()
            End Using
            If Not (IsDBNull(conn)) Then
                conn.Close()
            End If
        Catch
            conn.Close()
            If Not (IsDBNull(conn)) Then
                conn.Close()
            End If
            MessageBox.Show(Err.GetException.Message)
        End Try
    End Function
    Public Function RecordExist(ByVal sqlstring As String) As Boolean
        Try
            Dim count As Integer
            conn = connection.getConnection()
            Dim cmd As OleDbCommand = New OleDbCommand(sqlstring, conn)
            Using (cmd)
                count = cmd.ExecuteScalar()
                If (count > 0) Then
                    RecordExist = True
                    If Not (IsDBNull(conn)) Then
                        conn.Close()
                    End If
                    Exit Function
                Else
                    RecordExist = False
                End If
            End Using
            If Not (IsDBNull(conn)) Then
                conn.Close()
            End If
        Catch
            conn.Close()
            If Not (IsDBNull(conn)) Then
                conn.Close()
            End If
            MessageBox.Show(Err.GetException.Message)
        End Try
    End Function
    Public Sub LoadRecordsToControl(ByVal sqlString As String, ByVal control As Control)
        Try
            conn = connection.getConnection()
            Dim cmd As OleDbCommand
            cmd = New OleDbCommand(sqlString, conn)
            Dim reader As OleDbDataReader
            Dim da As OleDbDataAdapter
            Using (cmd)
                If (TypeOf control Is TextBox) Then
                    reader = cmd.ExecuteReader()
                    Dim txt As TextBox = TryCast(control, TextBox)
                    Using (reader)
                        While (reader.Read())
                            txt.Text = reader.GetValue(0)
                        End While
                        reader.Close()
                    End Using

                End If

                If (TypeOf control Is ComboBox) Then

                    reader = cmd.ExecuteReader()
                    Dim cb As ComboBox = control
                    Using (reader)
                        While (reader.Read())
                            cb.Items.Add(reader.GetValue(0))
                        End While
                        reader.Close()
                    End Using
                End If



                If (TypeOf control Is DataGridView) Then
                    Dim dv As DataGridView = control
                    Dim dt As New DataTable
                    da = New OleDbDataAdapter(cmd)

                    da.Fill(dt)
                    Using (dt)
                        dv.DataSource = dt
                    End Using

                End If
            End Using
            If Not (IsDBNull(conn)) Then
                conn.Close()
            End If
        Catch
            If Not (IsDBNull(conn)) Then
                conn.Close()
            End If
            MessageBox.Show(Err.GetException.Message)
        End Try
    End Sub
    Public Function ValidateLogin(ByVal UserName As String, ByVal Password As String) As Boolean
        Try
            Dim sqlStr = "SELECT COUNT(*) FROM Login WHERE LoginID='" + UserName + "' AND Password='" + Password + "'"
            conn = connection.getConnection()
            Dim cmd As OleDbCommand
            cmd = New OleDbCommand(sqlStr, conn)
            Dim reader As OleDbDataReader
            reader = cmd.ExecuteReader
            Using (reader)
                While (reader.Read())
                    If reader.GetValue(0) > 0 Then
                        ValidateLogin = True
                        If Not (IsDBNull(conn)) Then
                            conn.Close()
                        End If
                        Exit Function
                    End If
                End While
            End Using
            ValidateLogin = False
            If Not (IsDBNull(conn)) Then
                conn.Close()
            End If
        Catch
            MessageBox.Show(Err.GetException.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop)
        End Try
    End Function
End Class
