'Imports System.Data.DataTable
Imports System.Data.SqlClient
Imports System.ComponentModel  'Allows function of Datagridview sorting and filtering



Public Class frmJobEntry
    'THIS CREATS LOCAL INSTANCE TO REFRENCE THE SQL CLASS FORM, NOT USED WHEN WORKING WITH DATAGRIDVIEW
    Private SQL As New SQLConn


    '---------------------------------------    SETTING UP LOCAL INSTANCE FOR SQL LINK FOR DATAGRID TO SYNC CORRECTLY WITH SQL -------------------------------------
    Public LConn As New SQLConnection(My.Settings.SQLConn) 'This need to be changed in Project/Propertie/Settings
    Private LCmd As SQLCommand

    'SQL CONNECTORS
    Public LDA As SQLDataAdapter
    Public LDS As DataSet
    Public LDT As DataTable
    Public LCB As SQLCommandBuilder

    Public LRecordCount As Integer
    Private LException As String
    ' SQL QUERY PARAMETERS
    Public LParams As New List(Of SQLParameter)
    '-------------------------------------------------------------------------------------------------------------------------------------------------------------------------




    Public varUserName
    Public varJobNum
    Public varMachineCode
    Public varMachineName
    Public varProductCode
    Public varYear
    Public varMonth
    Public varDoffingNum
    Public varProductName
    Public varSpNums
    Public varPalBCode
    Public mergeNum As String
    Public PoyPalBarcode As String
    Public coneValUpdate As Integer
    Public JobBarcode As String
    Public varProdWeight As String
    Public varweightcode As String


    Dim machineName As String = ""
    Dim machineCode As String
    Dim productCode As String
    Dim year As String
    Dim month As String
    Dim doffingNum As String
    Dim quit As Integer



    Public PackOp As String
    Public PackSortOP As String
    Public changeCone As Integer
    Public time As DateTime = DateTime.Now
    Public Format As String = "dd mm yyyy  HH:mm"



    Private Sub frmJobEntry_Load(sender As Object, e As EventArgs) Handles MyBase.Load



        Me.txtLotNumber.Visible = False


        'If My.Settings.chkUsePack Then btnExChangeCone.Visible = True Else btnExChangeCone.Visible = False
        'If My.Settings.chkUsePack Then btnSearchCone.Visible = True Else btnSearchCone.Visible = False
        'If My.Settings.chkUsePack Then btnReports.Visible = True Else btnReports.Visible = False


        Me.KeyPreview = True  'Allows us to look for advace character from barcode

        'Set Form Header text

        Me.Text = "POY Job Entry Packing"





    End Sub

    Public Sub txtOperator_TextChanged(sender As Object, e As EventArgs) Handles txtOperator.TextChanged

        ComDrumLayer.Visible = True


        PackOp = txtOperator.Text
        varUserName = txtOperator.Text

    End Sub

    Private Sub ComDrumLayer_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComDrumLayer.SelectedIndexChanged

        txtLotNumber.Visible = True

    End Sub


    'Private Sub btnContinue_Click(sender As Object, e As EventArgs) Handles btnContinue.Click
    Private Sub prgContinue()

        Try


            If txtLotNumber.TextLength <> 10 Then  ' For carts B10,11 & 12
                MsgBox("This is not a Valid Palette Barcode. Please RE Scan")
                Me.txtLotNumber.Clear()
                Me.txtLotNumber.Focus()
                Me.txtLotNumber.Refresh()
                Exit Sub

            End If

        Catch ex As Exception
            MsgBox("BarCcode Is Not Valid")
            Me.txtLotNumber.Clear()
            Me.txtLotNumber.Focus()
            Me.txtLotNumber.Refresh()
            Exit Sub
        End Try

        CreateJob()

    End Sub

    Private Sub CreateJob()

        'GET YEAR AND MOTH FROM PALETTE STRING

        year = txtLotNumber.Text.Substring(1, 2)
        month = txtLotNumber.Text.Substring(3, 2)





        varPalBCode = txtLotNumber.Text


        varMachineCode = machineCode
        varMachineName = machineName
        varProductCode = productCode
        varYear = year
        varMonth = month
        varDoffingNum = doffingNum






        'Routine to change the scanned BARCODE to be the First CART not the secone cart and this is what will be stored in the DATABASE

        PoyPalBarcode = txtLotNumber.Text




        PackScree1()



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





    Private Sub PackScree1()


        'GET PRODUCT WEIGHT INFORMATION
        LExecQuery("SELECT * FROM POYtrack WHERE POYPALNUM = '" & varPalBCode & "'")

        MsgBox("This Pallete has already been started Do you wish to Continue or cancel")




        If LRecordCount > 0 Then

            MsgBox("This Pallete has already been started Do you wish to Continue or cancel")

            Dim result = MessageBox.Show("Edit Job Yes Or No", "JOB ALREADY EXISTS", MessageBoxButtons.YesNo, MessageBoxIcon.Information)

            If result = DialogResult.Yes Then
                'LOAD THE DATA FROM dB IN TO THE DATAGRID
                frmDGV.DGVdata.DataSource = LDS.Tables(0)
                frmDGV.DGVdata.Rows(0).Selected = True
                Dim LCB As SqlCommandBuilder = New SqlCommandBuilder(LDA)


                'SORT GRIDVIEW IN TO CORRECT CONE SEQUENCE
                frmDGV.DGVdata.Sort(frmDGV.DGVdata.Columns(5), ListSortDirection.Ascending)  'sorts On cone number

                coneValUpdate = 1
                Me.Hide()
                frmPacking.Show()
            End If

            If result = DialogResult.No Then
                Me.txtLotNumber.Clear()
                Me.txtLotNumber.Focus()
            End If

        Else


            Me.Hide()
            frmPacking.Show()

        End If


        Me.txtLotNumber.Clear()
            Me.txtLotNumber.Focus()



    End Sub



    Private Sub btnSettings_Click_1(sender As Object, e As EventArgs) Handles btnSettings.Click
        frmPassword.Show()
    End Sub





    Private Sub btnExChangeCone_Click(sender As Object, e As EventArgs) Handles btnExChangeCone.Click

        If txtOperator.Text = "" Then
            MsgBox("Please Enter Operator Name First")
        Else
            changeCone = 1
            Me.Hide()
            frmExChangeCone.Show()
        End If

    End Sub

    Private Sub btnSearchCone_Click(sender As Object, e As EventArgs) Handles btnSearchCone.Click
        If txtOperator.Text = "" Then
            MsgBox("Please Enter Operator Name First")
        Else
            Me.Hide()
            frmConeSearch.Show()
        End If
    End Sub



    Private Sub btnReports_Click(sender As Object, e As EventArgs) Handles btnReports.Click
        frmPackReports.Show()
    End Sub


    Private Sub frmJobEntry_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown

        If e.KeyCode = Keys.Return Then prgContinue()

    End Sub


End Class