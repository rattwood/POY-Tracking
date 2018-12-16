Imports System.Data.SqlClient

Public Class frmchangeTrace
    Dim bcodescan As String

    '---------------------------------------    SETTING UP LOCAL INSTANCE FOR SQL LINK FOR DATAGRID TO SYNC CORRECTLY WITH SQL -------------------------------------
    Public LConn As New SqlConnection(My.Settings.SQLConn) 'This need to be changed in Project/Propertie/Settings
    Private LCmd As SqlCommand

    'SQL CONNECTORS
    Public LDA As SqlDataAdapter
    Public LDS As DataSet
    Public LDT As DataTable
    Public LCB As SqlCommandBuilder

    Public LRecordCount As Integer
    Private LException As String
    ' SQL QUERY PARAMETERS
    Public LParams As New List(Of SqlParameter)

    Private Sub frmchangeTrace_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        'Get trace number form original form
        txtTraceNum.Text = frmToolEntry.txtTraceNum.Text.ToString
        txtTraceNum.Enabled = False


        txtNewTraceNum.Focus()

        Me.KeyPreview = True  'Allows us to look for advace character from barcode


    End Sub

    Private Sub txtNewTraceNum_TextChanged(sender As Object, e As EventArgs) Handles txtNewTraceNum.TextChanged
        bcodescan = txtNewTraceNum.Text.ToString

    End Sub


    Private Sub traceCheck()
        Try


            If Not (txtNewTraceNum.TextLength = 10) Then  ' LENGTH OF BARCODE
                lblError.Visible = True
                lblError.Text = "This is not a TRACE barcode" & vbCrLf & "Please RE Scan"
                DelayTM()
                lblError.Visible = False
                txtNewTraceNum.Clear()
                txtNewTraceNum.Focus()
                Exit Sub
            Else

                LExecQuery("Select * from POYTrack where (POYTRACENUM Is Not Null) and POYTRACENUM = '" & bcodescan & "' ")

                If LRecordCount > 0 Then

                    Me.KeyPreview = False  'Turn off Barcode entry

                    lblError.Visible = True
                    lblError.Text = "This TRACE is already used" & vbCrLf & "in the system"
                    DelayTM()
                    lblError.Visible = False

                    Me.KeyPreview = True  'Allows us to look for advace character from barcode

                    txtNewTraceNum.Clear()
                    txtNewTraceNum.Focus()
                    Exit Sub

                Else

                    LExecQuery("Select * from POYTrack where (POYTRACENUM Is Not Null) and POYTRACENUM = '" & txtTraceNum.Text.ToString & "' ORDER BY POYPACKIDX ")

                    Try
                        If LRecordCount > 0 Then
                            frmDGV.DGVdata.DataSource = LDS.Tables(0)
                            frmDGV.DGVdata.Rows(0).Selected = True
                            Dim LCB As SqlCommandBuilder = New SqlCommandBuilder(LDA)

                            Me.Cursor = System.Windows.Forms.Cursors.Default
                            lblError.Text = ""
                            lblError.Visible = False
                        End If
                    Catch ex As Exception
                        Me.Cursor = System.Windows.Forms.Cursors.Default
                        MsgBox("Job creation Error" & vbNewLine & ex.Message)
                    End Try

                    btnUpdate.Visible = True

                End If
            End If


        Catch ex As Exception
            'MsgBox("SQl Search Error " & vbNewLine & ex.Message)
            'txtTraceNum.Clear()
            'txtTraceNum.Focus()
            'Exit Sub
        End Try

    End Sub

    Private Sub DelayTM()
        Dim interval As Integer = "2000"
        Dim sw As New Stopwatch
        sw.Start()
        Do While sw.ElapsedMilliseconds < interval
            Application.DoEvents()
        Loop
        sw.Stop()

    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Close()
        frmToolEntry.Show()
    End Sub

    Private Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        txtTraceNum.Enabled = False
        lblComplete.Visible = False

        btnUpdate.Visible = False
        txtNewTraceNum.Clear()
        txtNewTraceNum.Focus()

    End Sub



    Private Sub btnUpdate_Click(sender As Object, e As EventArgs) Handles btnUpdate.Click

        For i = 1 To frmDGV.DGVdata.Rows.Count
            frmDGV.DGVdata.Rows(i - 1).Cells("POYTRACENUM").Value = txtNewTraceNum.Text.ToString
            'LExecQuery("update POYTRACK set POYTRACENUM = '" & txtNewTraceNum.Text.ToString & "' where POYTRACENUM = '" & txtTraceNum.Text.ToString & "' ")
        Next

        bcodescan = txtNewTraceNum.Text.ToString
        UpdateDatabase()

        frmToolEntry.txtTraceNum.Text = txtNewTraceNum.Text.ToString
        lblComplete.Visible = True
        frmToolEntry.lblTraceComplete.Visible = True
        frmToolEntry.updateAfterTraceChange()
        frmToolEntry.Show()
        Close()
    End Sub

    Public Sub LExecQuery(Query As String)
        ' RESET QUERY STATISTCIS
        LRecordCount = 0
        LException = ""


        If LConn.State = ConnectionState.Open Then LConn.Close()
        Try

            'OPEN SQL DATABSE CONNECTION
            LConn.Open()

            'CREATE SQL COMMAND
            LCmd = New SqlCommand(Query, LConn)

            'LOAD PARAMETER INTO SQL COMMAND
            LParams.ForEach(Sub(p) LCmd.Parameters.Add(p))

            'CLEAR PARAMETER LIST
            LParams.Clear()

            'EXECUTE COMMAND AND FILL DATASET
            LDS = New DataSet
            LDT = New DataTable
            LDA = New SqlDataAdapter(LCmd)

            LRecordCount = LDA.Fill(LDS)

        Catch ex As Exception

            LException = "ExecQuery Error: " & vbNewLine & ex.Message
            MsgBox(LException)

        End Try

    End Sub


    Public Sub UpdateDatabase()

        tsbtnSave()





        '******************   THIS WILL WRITE ANY CHANGES MADE TO THE DATAGRID BACK TO THE DATABASE ******************

        Try

            If LDS.HasChanges Then


                'LDA.UpdateCommand = New Oracle.ManagedDataAccess.Client.OracleCommandBuilder(frmJobEntry.LDA).GetUpdateCommand

                LDA.Update(LDS.Tables(0))

            End If
        Catch ex As Exception

            MsgBox("Update Error: " & vbNewLine & ex.Message)
        End Try







    End Sub

    Public Sub tsbtnSave()


        Dim bAddState As Boolean = frmDGV.DGVdata.AllowUserToAddRows

        frmDGV.DGVdata.AllowUserToAddRows = True
        frmDGV.DGVdata.CurrentCell = frmDGV.DGVdata.Rows(frmDGV.DGVdata.Rows.Count - 1).Cells(0) ' move to add row
        frmDGV.DGVdata.CurrentCell = frmDGV.DGVdata.Rows(0).Cells(0) ' move back to current row  Changed Rows(iRow) to (0)
        frmDGV.DGVdata.AllowUserToAddRows = bAddState



    End Sub

    'THIS LOOKS FOR ENTER key to be pressed or received via barcode
    Private Sub frmTraceEntry_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown

        If e.KeyCode = Keys.Return Then


            traceCheck()


        End If

    End Sub
End Class