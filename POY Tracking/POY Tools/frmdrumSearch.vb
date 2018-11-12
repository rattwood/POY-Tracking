Imports System.ComponentModel

Public Class frmdrumSearch
    Dim dbDate As Date
    Dim datestring As String


    Private Sub frmConeSearch_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        'txtBoxJob.Text = ""
        txtBoxConeBC.Text = ""
        'txtBoxSpindle.Text = ""
        'txtBoxSpindle.Enabled = False
        'btnJobSearch.Enabled = False
        btnDrumSearch.Enabled = False

        Me.KeyPreview = True  'Allows us to look for advace character from barcode

        'txtBoxJob.Focus()



    End Sub




    Private Sub btnDrumSearch_Click(sender As Object, e As EventArgs) Handles btnDrumSearch.Click

        frmJobEntry.LExecQuery("SELECT POYMCNUM, POYPRODNAME, POYDOFFNUM, POYMERGENUM, POYPACKDATE, POYPACKNAME, POYTRACENUM, " _
                               & "POYSTEPNUM, POYPACKIDX, POYPRODWEIGHT FROM POYTrack Where POYBCODEDRUM  = '" & txtBoxConeBC.Text & "' ")

        If frmJobEntry.LRecordCount > 0 Then
            frmDGV.DGVdata.DataSource = frmJobEntry.LDS.Tables(0)
            frmDGV.DGVdata.Rows(0).Selected = True
            jobSearch()
            'Else
            '    MsgBox("Cone #: " & txtBoxSpindle.Text & "  Cannot be found")
            '    Me.txtBoxJob.Clear()
            '    Me.txtBoxSpindle.Clear()
            '    Me.btnJobSearch.Enabled = False
            '    Me.txtBoxJob.Focus()
            '    Me.txtBoxJob.Refresh()
            '    Exit Sub
        End If

    End Sub

    Private Sub jobSearch()


        'LOAD THE DATA FROM dB IN TO THE DATAGRID
        DataGridView1.DataSource = frmJobEntry.LDS.Tables(0)
            DataGridView1.Rows(0).Selected = True

            'SORT GRIDVIEW IN TO CORRECT JOB SEQUENCE
            'DataGridView1.Sort(DataGridView1.Columns("POYBCODEDRUM"), ListSortDirection.Ascending)  'sorts On cone number
            'frmPrintCartReport.Show()

            'PRODUCT NAME

            txtBoxProdName.Text = DataGridView1.Rows(0).Cells("POYPRODNAME").Value
            'MERGENUM
            txtMergeNum.Text = DataGridView1.Rows(0).Cells("POYMERGENUM").Value

            'Machine NUMBER
            txtBoxMCNum.Text = DataGridView1.Rows(0).Cells("POYMCNUM").Value

            'DATE
            dbDate = DataGridView1.Rows(0).Cells("POYPACKDATE").Value.ToString
            dateConv()
            txtBoxPackDate.Text = datestring

            'DOFFING NUMBER
            txtBoxDoff.Text = DataGridView1.Rows(0).Cells("POYDOFFNUM").Value

            'Packer
            txtBoxPacker.Text = DataGridView1.Rows(0).Cells("POYPACKNAME").Value

            'TRACE Number
            txtTraceNum.Text = DataGridView1.Rows(0).Cells("POYTRACENUM").Value

            'TRACE Number
            txtTraceNum.Text = DataGridView1.Rows(0).Cells("POYTRACENUM").Value

            'STEP Number
            txtStepNum.Text = DataGridView1.Rows(0).Cells("POYSTEPNUM").Value

            'Packing Index number
            txtIdxNum.Text = DataGridView1.Rows(0).Cells("POYPACKIDX").Value

            'Packing Index number
            txtIdxNum.Text = DataGridView1.Rows(0).Cells("POYPACKIDX").Value

            'MACHINE NUMBER
            txtWeight.Text = DataGridView1.Rows(0).Cells("POYPRODWEIGHT").Value
       

    End Sub

    Public Sub dateConv()

        Try
            datestring = dbDate.ToString("dd/MM/yyyy")
        Catch ex As Exception
            MsgBox("Date Missing in Database")
        End Try




    End Sub



    Private Sub txtBoxConeBC_TextChanged(sender As Object, e As EventArgs) Handles txtBoxConeBC.TextChanged
        ' txtBoxJob.Clear()
        btnDrumSearch.Enabled = True

    End Sub



    Private Sub frmConeSearch_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown

        If e.KeyCode = Keys.Return Then

        End If
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click

        Me.Close()
        frmJobEntry.Show()

    End Sub




End Class