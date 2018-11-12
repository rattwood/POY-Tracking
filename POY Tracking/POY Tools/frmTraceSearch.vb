Imports System.ComponentModel



Public Class frmTraceSearch

    Dim dbDate As Date
        Dim datestring As String


    Private Sub frmTraceSearch_Load(sender As Object, e As EventArgs) Handles MyBase.Load


        txtTraceNum.Text = ""

        'txtBoxSpindle.Text = ""
        'txtBoxSpindle.Enabled = False
        btnJobSearch.Enabled = False


        Me.KeyPreview = True  'Allows us to look for advace character from barcode

        txtTraceNum.Focus()


    End Sub


    Private Sub btnJobSearch_Click(sender As Object, e As EventArgs) Handles btnJobSearch.Click

        If txtTraceNum.TextLength > 10 Or txtTraceNum.TextLength < 10 Then
            MsgBox("Job number is not the correct length")
            Me.txtTraceNum.Clear()

            Me.btnJobSearch.Enabled = False
            Me.txtTraceNum.Focus()
            Me.txtTraceNum.Refresh()
            Exit Sub
        End If

        Try

            frmJobEntry.LExecQuery("SELECT POYSTEPNUM, POYPACKIDX, POYDOFFNUM, POYSPINNUM, POYBCODEDRUM, " _
                               & "POYPRODWEIGHT FROM POYTrack Where POYTRACENUM  = '" & txtTraceNum.Text & "' ORDER BY poypackidx ")



            If frmJobEntry.LRecordCount > 0 Then

                jobSearch()

            Else
                MsgBox("Job: " & txtTraceNum.Text & "  Cannot be found")
                Me.txtTraceNum.Clear()

                Me.btnJobSearch.Enabled = False
                Me.txtTraceNum.Focus()
                Me.txtTraceNum.Refresh()
                Exit Sub

            End If

        Catch ex As Exception
            MsgBox(ex.ToString)
            Me.txtTraceNum.Clear()
            Me.btnJobSearch.Enabled = False
            Me.txtTraceNum.Focus()
            Me.txtTraceNum.Refresh()
            Exit Sub
        End Try


    End Sub


    Private Sub jobSearch()

        'LOAD THE DATA FROM dB IN TO THE DATAGRID
        DataGridView1.DataSource = frmJobEntry.LDS.Tables(0)
        DataGridView1.Rows(0).Selected = True


        DataGridView1.Visible = True




    End Sub

    Public Sub dateConv()

        Try
            datestring = dbDate.ToString("dd/MM/yyyy")
        Catch ex As Exception
            MsgBox("Date Missing in Database")
        End Try




    End Sub

    Private Sub txtBoxJob_TextChanged(sender As Object, e As EventArgs) Handles txtTraceNum.TextChanged


        btnJobSearch.Enabled = True

    End Sub




    Private Sub frmDrumSearch_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown

        If e.KeyCode = Keys.Return Then

        End If
    End Sub



    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Me.Close()
        frmJobEntry.Show()
    End Sub
End Class