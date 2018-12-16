Public Class frmPalletSplit



    Private Sub frmPalletSplit_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        txtDrumNum.Text = ""

        'txtBoxSpindle.Text = ""
        'txtBoxSpindle.Enabled = False
        btnJobSearch.Enabled = False


        Me.KeyPreview = True  'Allows us to look for advace character from barcode

        txtDrumNum.Focus()
    End Sub

    Private Sub btnJobSearch_Click(sender As Object, e As EventArgs) Handles btnJobSearch.Click

        If txtDrumNum.TextLength < 10 Then
            MsgBox("Job number is not the correct length")
            Me.txtDrumNum.Clear()

            Me.btnJobSearch.Enabled = False
            Me.txtDrumNum.Focus()
            Me.txtDrumNum.Refresh()
            Exit Sub
        ElseIf txtDrumNum.TextLength = 11 Then
            Dim chkString = txtDrumNum.Text.Substring(0, 1)
            If Not chkString = "P" Then
                MsgBox("Trace Number is not the correct")
                Me.txtDrumNum.Clear()
                Me.btnJobSearch.Enabled = False
                Me.txtDrumNum.Focus()
                Me.txtDrumNum.Refresh()
                Exit Sub
            End If
        End If

        Try

            frmJobEntry.LExecQuery("SELECT POYSTEPNUM, POYPACKIDX, POYDOFFNUM, POYSPINNUM, POYBCODEDRUM, " _
                               & "POYPRODWEIGHT FROM POYTrack Where POYTRACENUM  = '" & txtDrumNum.Text & "' ORDER BY poypackidx ")



            If frmJobEntry.LRecordCount > 0 Then

                jobSearch()

            Else
                MsgBox("Job: " & txtDrumNum.Text & "  Cannot be found")
                Me.txtDrumNum.Clear()

                Me.btnJobSearch.Enabled = False
                Me.txtDrumNum.Focus()
                Me.txtDrumNum.Refresh()
                Exit Sub

            End If

        Catch ex As Exception
            MsgBox(ex.ToString)
            Me.txtDrumNum.Clear()
            Me.btnJobSearch.Enabled = False
            Me.txtDrumNum.Focus()
            Me.txtDrumNum.Refresh()
            Exit Sub
        End Try


    End Sub

    Private Sub jobSearch()

        'LOAD THE DATA FROM dB IN TO THE DATAGRID
        DataGridView1.DataSource = frmJobEntry.LDS.Tables(0)
        DataGridView1.Rows(0).Selected = True


        DataGridView1.Visible = True




    End Sub


End Class