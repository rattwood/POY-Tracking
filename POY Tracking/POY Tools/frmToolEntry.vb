Imports System.Data.SqlClient

Public Class frmToolEntry
    Public SQL As New SQLConn
    Dim bcodescan As String



    Private Sub frmToolEntry_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        txtTraceNum.Clear()
        txtTraceNum.Focus()


        Me.KeyPreview = True  'Allows us to look for advace character from barcode

    End Sub

    Private Sub txtTraceNum_TextChanged(sender As Object, e As EventArgs) Handles txtTraceNum.TextChanged
        bcodescan = txtTraceNum.Text.ToString
    End Sub

    Private Sub traceCheck()
        Try


            If Not (txtTraceNum.TextLength = 10) Then  ' LENGTH OF BARCODE
                lblError.Visible = True
                lblError.Text = "This is not a TRACE barcode" & vbCrLf & "Please RE Scan"
                DelayTM()
                lblError.Visible = False
                txtTraceNum.Clear()
                txtTraceNum.Focus()
                Exit Sub
            Else

                SQL.ExecQuery("Select * from POYTrack where (POYTRACENUM Is Not Null) and POYTRACENUM = '" & bcodescan & "' and POYBCODEDRUM Is Not Null Order by POYPACKIDX ")

                If SQL.RecordCount = 0 Then

                    Me.KeyPreview = False  'Turn off Barcode entry

                    lblError.Visible = True
                    lblError.Text = "This TRACE is not in the system"
                    DelayTM()
                    lblError.Visible = False

                    Me.KeyPreview = True  'Allows us to look for advace character from barcode

                    txtTraceNum.Clear()
                    txtTraceNum.Focus()
                    Exit Sub

                Else
                    Try
                        If SQL.RecordCount > 0 Then
                            frmDGV.DGVdata.DataSource = SQL.SQLDS.Tables(0)
                            frmDGV.DGVdata.Rows(0).Selected = True
                            Dim LCB As SqlCommandBuilder = New SqlCommandBuilder(SQL.SQLDA)

                            Me.Cursor = System.Windows.Forms.Cursors.Default
                            lblError.Text = ""
                            lblError.Visible = False
                        End If
                    Catch ex As Exception
                        Me.Cursor = System.Windows.Forms.Cursors.Default
                        MsgBox("Job creation Error" & vbNewLine & ex.Message)
                    End Try

                    Label3.Visible = True
                    Label5.Visible = True
                    Label7.Visible = True
                    Label9.Visible = True
                    Label11.Visible = True

                    lblProduct.Visible = True
                    lblMerge.Visible = True
                    lblDate.Visible = True
                    lblPalSize.Visible = True
                    lblDrums.Visible = True

                    lblProduct.Text = frmDGV.DGVdata.Rows(0).Cells("POYPRODNAME").Value
                    lblMerge.Text = frmDGV.DGVdata.Rows(0).Cells("POYMERGENUM").Value
                    lblDate.Text = frmDGV.DGVdata.Rows(0).Cells("POYPACKDATE").Value
                    lblPalSize.Text = frmDGV.DGVdata.Rows(0).Cells("POYDRUMPERPAL").Value
                    lblDrums.Text = SQL.RecordCount


                    btnChangeDrum.Enabled = True
                    btnChangeDrum.BackColor = Color.LightGreen
                    btnChangeSteps.Enabled = True
                    btnChangeSteps.BackColor = Color.LightGreen
                    btnChangeTrace.Enabled = True
                    btnChangeTrace.BackColor = Color.LightGreen
                    txtTraceNum.Enabled = False

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

    Private Sub btnChangeSteps_Click(sender As Object, e As EventArgs) Handles btnChangeSteps.Click
        lblComplete.Visible = False
        Dim result = MessageBox.Show("Edit Job Yes Or No", "Are you sure you wish to change all the STEP numbers", MessageBoxButtons.YesNo, MessageBoxIcon.Information)





        If result = DialogResult.Yes Then



            SQL.ExecQuery("Select * from POYTrack where (POYTRACENUM Is Not Null) and POYTRACENUM = '" & bcodescan & "'  Order by POYPACKIDX ")

            If SQL.RecordCount > 0 Then

                Try
                    If SQL.RecordCount > 0 Then
                        frmDGV.DGVdata.DataSource = SQL.SQLDS.Tables(0)
                        frmDGV.DGVdata.Rows(0).Selected = True
                        Dim LCB As SqlCommandBuilder = New SqlCommandBuilder(SQL.SQLDA)

                        Me.Cursor = System.Windows.Forms.Cursors.Default
                        lblError.Text = ""
                        lblError.Visible = False
                    End If
                Catch ex As Exception
                    Me.Cursor = System.Windows.Forms.Cursors.Default
                    MsgBox("Job creation Error" & vbNewLine & ex.Message)
                End Try

            End If

            Dim tmpcount As Integer = frmDGV.DGVdata.Rows.Count
            Dim idxReverse As Integer = frmDGV.DGVdata.Rows(0).Cells("POYDRUMPERPAL").Value
            Dim fmt As String = "000"
            Dim modIdxNum As String
            Dim startcount As Integer
            Dim endcount As Integer
            Dim rcount As Integer


            'Round 1 change to tmp1,2,3,4,5 & 6
            For i = 1 To tmpcount

                'Advance without writing a value if no Drum
                If IsDBNull(frmDGV.DGVdata.Rows(i - 1).Cells("POYSTEPNUM").Value) Then Continue For

                Select Case frmDGV.DGVdata.Rows(i - 1).Cells("POYSTEPNUM").Value.ToString
                    Case 1
                        frmDGV.DGVdata.Rows(i - 1).Cells("POYSTEPNUM").Value = "tmp1"
                    Case 2
                        frmDGV.DGVdata.Rows(i - 1).Cells("POYSTEPNUM").Value = "tmp2"
                    Case 3
                        frmDGV.DGVdata.Rows(i - 1).Cells("POYSTEPNUM").Value = "tmp3"
                    Case 4
                        frmDGV.DGVdata.Rows(i - 1).Cells("POYSTEPNUM").Value = "tmp4"
                    Case 5
                        frmDGV.DGVdata.Rows(i - 1).Cells("POYSTEPNUM").Value = "tmp5"
                    Case 6
                        frmDGV.DGVdata.Rows(i - 1).Cells("POYSTEPNUM").Value = "tmp6"
                End Select
            Next

            For i = 1 To tmpcount
                Select Case frmDGV.DGVdata.Rows(i - 1).Cells("POYSTEPNUM").Value.ToString
                    Case "tmp1"
                        frmDGV.DGVdata.Rows(i - 1).Cells("POYSTEPNUM").Value = 6

                        If idxReverse = 120 Then
                            startcount = 101
                            endcount = 20
                        ElseIf idxReverse = 72 Then
                            startcount = 61
                            endcount = 12
                        ElseIf idxReverse = 48 Then
                            startcount = 41
                            endcount = 8
                        End If

                        For rcount = 1 To endcount

                            modIdxNum = startcount.ToString(fmt)
                            frmDGV.DGVdata.Rows(rcount - 1).Cells("POYPACKIDX").Value = modIdxNum
                            startcount = startcount + 1

                        Next

                    Case "tmp2"
                        frmDGV.DGVdata.Rows(i - 1).Cells("POYSTEPNUM").Value = 5

                        If idxReverse = 120 Then
                            startcount = 81
                            endcount = 20
                        ElseIf idxReverse = 72 Then
                            startcount = 61
                            endcount = 12
                        ElseIf idxReverse = 48 Then
                            startcount = 33
                            endcount = 8
                        End If

                        For rcount = 1 To endcount

                            modIdxNum = startcount.ToString(fmt)
                            frmDGV.DGVdata.Rows(rcount - 1).Cells("POYPACKIDX").Value = modIdxNum
                            startcount = startcount + 1

                        Next

                    Case "tmp3"
                        frmDGV.DGVdata.Rows(i - 1).Cells("POYSTEPNUM").Value = 4


                        If idxReverse = 120 Then
                            startcount = 61
                            endcount = 20
                        ElseIf idxReverse = 72 Then
                            startcount = 61
                            endcount = 12
                        ElseIf idxReverse = 48 Then
                            startcount = 25
                            endcount = 8
                        End If

                        For rcount = 1 To endcount

                            modIdxNum = startcount.ToString(fmt)
                            frmDGV.DGVdata.Rows(rcount - 1).Cells("POYPACKIDX").Value = modIdxNum
                            startcount = startcount + 1

                        Next




                    Case "tmp4"
                        frmDGV.DGVdata.Rows(i - 1).Cells("POYSTEPNUM").Value = 3



                        If idxReverse = 120 Then
                            startcount = 41
                            endcount = 20
                        ElseIf idxReverse = 72 Then
                            startcount = 61
                            endcount = 12
                        ElseIf idxReverse = 48 Then
                            startcount = 17
                            endcount = 8
                        End If

                        For rcount = 1 To endcount

                            modIdxNum = startcount.ToString(fmt)
                            frmDGV.DGVdata.Rows(rcount - 1).Cells("POYPACKIDX").Value = modIdxNum
                            startcount = startcount + 1

                        Next


                    Case "tmp5"
                        frmDGV.DGVdata.Rows(i - 1).Cells("POYSTEPNUM").Value = 2

                        If idxReverse = 120 Then
                            startcount = 21
                        ElseIf idxReverse = 72 Then
                            startcount = 61
                            endcount = 12
                        ElseIf idxReverse = 48 Then
                            startcount = 9
                            endcount = 8
                        End If

                        For rcount = 1 To endcount

                            modIdxNum = startcount.ToString(fmt)
                            frmDGV.DGVdata.Rows(rcount - 1).Cells("POYPACKIDX").Value = modIdxNum
                            startcount = startcount + 1

                        Next


                    Case "tmp6"
                        frmDGV.DGVdata.Rows(i - 1).Cells("POYSTEPNUM").Value = 1


                        If idxReverse = 120 Then
                            startcount = 1
                            endcount = 20
                        ElseIf idxReverse = 72 Then
                            startcount = 61
                            endcount = 12
                        ElseIf idxReverse = 48 Then
                            startcount = 1
                            endcount = 8
                        End If

                        For rcount = 1 To endcount

                            modIdxNum = startcount.ToString(fmt)
                            frmDGV.DGVdata.Rows(rcount - 1).Cells("POYPACKIDX").Value = modIdxNum
                            startcount = startcount + 1

                        Next

                End Select
            Next



            'UpdateDatabase()

            lblComplete.Visible = True
            Exit Sub
        End If

        If result = DialogResult.No Then

            Exit Sub

        End If


    End Sub



    Private Sub btnChangeDrum_Click(sender As Object, e As EventArgs) Handles btnChangeDrum.Click
        Hide()
        frmChangeDrums.Show()
        smalldbUpdate()

    End Sub

    Private Sub btnChangeTrace_Click(sender As Object, e As EventArgs) Handles btnChangeTrace.Click
        frmchangeTrace.txtNewTraceNum.Clear()
        frmchangeTrace.txtNewTraceNum.Focus()
        frmchangeTrace.Show()
        bcodescan = txtTraceNum.Text.ToString  'to get updated trace number
        smalldbUpdate()

    End Sub

    Private Sub smalldbUpdate()
        'This routine is to refresh DGV with data for new Trace Number assigned
        SQL.ExecQuery("Select * from POYTrack where (POYTRACENUM Is Not Null) and POYTRACENUM = '" & bcodescan & "' and POYBCODEDRUM Is Not Null Order by POYPACKIDX ")
        Try
            If SQL.RecordCount > 0 Then
                frmDGV.DGVdata.DataSource = SQL.SQLDS.Tables(0)
                frmDGV.DGVdata.Rows(0).Selected = True
                Dim LCB As SqlCommandBuilder = New SqlCommandBuilder(SQL.SQLDA)

                Me.Cursor = System.Windows.Forms.Cursors.Default
                lblError.Text = ""
                lblError.Visible = False
            End If
        Catch ex As Exception
            Me.Cursor = System.Windows.Forms.Cursors.Default
            MsgBox("small update Error" & vbNewLine & ex.Message)
        End Try

    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click

        txtTraceNum.Clear()
        txtTraceNum.Focus()

        frmJobEntry.Show()
        Close()



    End Sub

    Private Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        txtTraceNum.Enabled = True
        txtTraceNum.Clear()
        txtTraceNum.Focus()

        Label3.Visible = False
        Label5.Visible = False
        Label7.Visible = False
        Label9.Visible = False
        Label11.Visible = False
        lblProduct.Visible = False
        lblMerge.Visible = False
        lblDate.Visible = False
        lblPalSize.Visible = False
        lblDrums.Visible = False
        lblTraceComplete.Visible = False

        btnChangeDrum.Enabled = False
        btnChangeDrum.BackColor = Color.LightGray
        btnChangeSteps.Enabled = False
        btnChangeSteps.BackColor = Color.LightGray
        btnChangeTrace.Enabled = False
        btnChangeTrace.BackColor = Color.LightGray

    End Sub

    Public Sub UpdateDatabase()

        tsbtnSave()





        '******************   THIS WILL WRITE ANY CHANGES MADE TO THE DATAGRID BACK TO THE DATABASE ******************

        Try

            If SQL.SQLDS.HasChanges Then


                'LDA.UpdateCommand = New Oracle.ManagedDataAccess.Client.OracleCommandBuilder(frmJobEntry.LDA).GetUpdateCommand

                SQL.SQLDA.Update(SQL.SQLDS.Tables(0))

            End If
        Catch ex As Exception

            MsgBox("Update Error: " & vbNewLine & ex.Message)
        End Try




        'Reload the DGV with new data that was written to the Database
        SQL.ExecQuery("Select * from POYTrack where (POYTRACENUM Is Not Null) and POYTRACENUM = '" & bcodescan & "' and POYBCODEDRUM Is Not Null Order by POYPACKIDX ")


        frmDGV.DGVdata.DataSource = SQL.SQLDS.Tables(0)
        frmDGV.DGVdata.Rows(0).Selected = True
        Dim LCB As SqlCommandBuilder = New SqlCommandBuilder(SQL.SQLDA)





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