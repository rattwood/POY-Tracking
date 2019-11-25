Imports System.Data.SqlClient
Imports System.IO
Imports System.Text
Imports System.Windows.Forms
Imports System.Diagnostics

Public Class frmTraceEntry
    Public bcodeScan As String
    Dim poydrums As Integer
    Dim SQL As New SQLConn


    Private Sub frmTraceEntry_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        frmJobEntry.Hide()
        txtTraceNum.Clear()
        txtTraceNum.Focus()

        lblTmpTraceNum.Text = frmDGV.DGVdata.Rows(0).Cells("POYTMPTRACE").Value

        Me.KeyPreview = True  'Allows us to look for advace character from barcode

    End Sub


    Private Sub prgContinue()


        poydrums = frmDGV.DGVdata.Rows(0).Cells("POYDRUMPERPAL").Value

        bcodeScan = txtTraceNum.Text.ToString


        Try

            If bcodeScan.Length = 12 And bcodeScan.Substring(0, 1) <> "P" Then
                lblError.Visible = True
                lblError.Text = "This is not a TRACE barcode" & vbCrLf & "Please RE Scan"
                DelayTM()
                lblError.Visible = False
                txtTraceNum.Clear()
                txtTraceNum.Focus()
                Exit Sub
            ElseIf bcodeScan.Length > 12 Or bcodeScan.Length < 10 Then
                lblError.Visible = True
                lblError.Text = "This is not a TRACE barcode" & vbCrLf & "Please RE Scan"
                DelayTM()
                lblError.Visible = False
                txtTraceNum.Clear()
                txtTraceNum.Focus()
                Exit Sub

            Else

                SQL.ExecQuery("Select * from POYPackTrace where (POYTRACENUM Is Not Null) and POYTRACENUM = '" & bcodeScan & "' ")

                If SQL.RecordCount > 0 Then

                    Me.KeyPreview = False  'Turn off Barcode entry

                    lblError.Visible = True
                    lblError.Text = "This TRACE number has" & vbCrLf & " already been used"
                    DelayTM()
                    lblError.Visible = False

                    Me.KeyPreview = True  'Allows us to look for advace character from barcode

                    txtTraceNum.Clear()
                    txtTraceNum.Focus()
                    Exit Sub

                Else

                    btnUpdate.Visible = True
                End If
            End If


        Catch ex As Exception

        End Try




    End Sub

    Private Sub btnUpdate_Click(sender As Object, e As EventArgs) Handles btnUpdate.Click
        'writes trace number against all drums

        For i = 1 To poydrums

            frmDGV.DGVdata.Rows(i - 1).Cells("POYTRACENUM").Value = bcodeScan 'Write trace to POYPACKTRACE
            If frmDGV.DGVdata.Rows(i - 1).Cells("POYTRACENUM").Value = frmDGV.DGVdata.Rows(0).Cells("POYTMPTRACE").Value Then _
                frmDGV.DGVdata.Rows(i - 1).Cells("POYTRACENUM").Value = bcodeScan 'WRITE TRACE NUMBER TO POYPACKIN

        Next


        Me.Cursor = System.Windows.Forms.Cursors.WaitCursor
        frmPackRepMain.PackRepMainSub()
        Me.Cursor = System.Windows.Forms.Cursors.Default
        jobEntryScreen()
        Close()



    End Sub

    Private Sub jobEntryScreen()
        'go back to Job Entry after creating excel sheet
        frmJobEntry.Label4.Visible = False
        frmJobEntry.comBoxDrumPal.Visible = False
        frmJobEntry.comBoxDrumPal.SelectedIndex = -1 'Blank the value so operater has to select


        frmJobEntry.Label2.Visible = False
        frmJobEntry.txtDrumNum.Visible = False
        frmJobEntry.txtDrumNum.Clear()
        frmJobEntry.txtDrumNum.Refresh()



        frmJobEntry.btnNewPallet.BackColor = Color.LightBlue
        frmJobEntry.btnNewPallet.Enabled = True
        frmJobEntry.btnOldPallet.BackColor = Color.LightBlue
        frmJobEntry.btnOldPallet.Enabled = True
        frmJobEntry.newJobFlag = 0

        frmJobEntry.lblAutoCorrect.Visible = False
        frmJobEntry.comBoxDrumPal.Enabled = True
        Me.Cursor = System.Windows.Forms.Cursors.Default
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





    'THIS LOOKS FOR ENTER key to be pressed or received via barcode
    Private Sub frmTraceEntry_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown

        If e.KeyCode = Keys.Return Then

            prgContinue()


        End If

    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click

        Select Case poydrums

            Case = 48
                frmPacking48.txtConeBcode.Clear()
                frmPacking48.txtConeBcode.Focus()
                frmPacking48.Show()
            Case = 72
                frmPacking72.txtConeBcode.Clear()
                frmPacking72.txtConeBcode.Focus()
                frmPacking72.Show()
            Case = 120
                frmPacking120.txtConeBcode.Clear()
                frmPacking120.txtConeBcode.Focus()
                frmPacking120.Show()
        End Select

        Close()


    End Sub


End Class