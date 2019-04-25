Imports System.Data.SqlClient

Public Class frmSelectRelease

    'THIS CREATS LOCAL INSTANCE TO REFRENCE THE SQL CLASS FORM, NOT USED WHEN WORKING WITH DATAGRIDVIEW
    'Private SQL As New SQLConn
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
    '------------------------------------------------------

    'Get Grade type from frmHolRelMethod
    Dim tmpRelGrade As String = frmHoldRelMethod.varRelGrade
    Dim tmpOpName As String = frmHoldRelMethod.txtBoxOpName.Text
    Dim firstDrum As Integer = 0
    Dim tmpBcode As String
    Dim tmpProdNum As String
    Dim tmpProdName As String
    Dim tmpMergeNum As String

    'variables for Richtextbox
    Dim tmpIdxCount As Integer

    'TIME
    Dim time As New DateTime
    Dim dateFormat As String = "yyyy-MM-dd HH:mm:ss"
    Dim todayTimeDate As String


    Private Sub frmSelectRelease_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'SET OPERATOR NAME ON SCREEN
        lblOpName.Text = tmpOpName
        'SET RELEASE GRADE ON SCREEN
        lblRelGrade.Text = tmpRelGrade

        'Set drum count to Zero
        lblDrumCount.Text = 0
        SplitContainer1.Panel2.Visible = False


        'Me.KeyPreview = True  'Allows us to look for advance character from barcode
        Me.KeyPreview = True

        txtBoxScanDrum.Clear()
        txtBoxScanDrum.Refresh()

        txtBoxDrumBcode.Clear()
        txtBoxDrumBcode.Focus()
        txtBoxDrumBcode.Refresh()


    End Sub


    Private Sub prgContinue()




        tmpBcode = txtBoxDrumBcode.Text

            If txtBoxDrumBcode.TextLength > 14 Or txtBoxDrumBcode.TextLength < 14 Then
                lblMessage.Visible = True
                lblMessage.Text = "This Is NOT a DRUM Barcode" & vbCrLf & "Please check"
                DelayTM()
                lblMessage.Visible = False

                txtBoxDrumBcode.Clear()
                txtBoxDrumBcode.Focus()
                Exit Sub
            End If

        If firstDrum = 1 Then   'check product number
            Dim tmpScanProdNum As String

            tmpScanProdNum = tmpBcode.Substring(2, 3)

            If tmpScanProdNum <> tmpProdNum Then
                lblMessage.Visible = True
                lblMessage.Text = "This DRUM is wrong product Group " & vbCrLf & "Please check DRUM Barcode"
                DelayTM()
                lblMessage.Visible = False

                txtBoxDrumBcode.Clear()
                txtBoxDrumBcode.Focus()
                Exit Sub

            End If

        End If


        '  LExecQuery("Select * from poytrack2 Where poybcodedrum = '" & tmpBcode & "' and POYDRUMSTATE = 4 ")
        LExecQuery("Select * from poytrack2 Where poybcodedrum = '" & tmpBcode & "' and POYDRUMSTATE = 4 and (POYMISSDRUM is NUll or POYMISSDRUM < 1) ")



        If LRecordCount > 0 Then
                'LOAD THE DATA FROM dB IN TO THE DATAGRID
                frmDGV.DGVdata.DataSource = LDS.Tables(0)
                frmDGV.DGVdata.Rows(0).Selected = True
                createFormScreen()

            Else
                lblMessage.Visible = True
                lblMessage.Text = "This DRUM is not on Hold" & vbCrLf & "Please check"
                DelayTM()
                lblMessage.Visible = False
                txtBoxDrumBcode.Clear()
                txtBoxDrumBcode.Focus()
                Exit Sub
            End If


    End Sub

    Private Sub createFormScreen()


        If firstDrum = 0 Then

            'LOAD THE DATA FROM dB IN TO THE DATAGRID
            frmDGV.DGVdata.DataSource = LDS.Tables(0)
            frmDGV.DGVdata.Rows(0).Selected = True



            'SET PRODUCT NAME ON SCREEN
            tmpProdName = frmDGV.DGVdata.Rows(0).Cells("POYPRODNAME").Value.ToString
            tmpMergeNum = frmDGV.DGVdata.Rows(0).Cells("POYMERGENUM").Value.ToString
            lblProdName.Text = tmpProdName & " " & tmpMergeNum

            'GET PRODUCT NUMBER
            tmpProdNum = frmDGV.DGVdata.Rows(0).Cells("POYPRNUM").Value.ToString
            lblProdNum.Text = tmpProdNum


            txtBoxScanDrum.Clear()
            txtBoxScanDrum.Refresh()

            txtBoxScanDrum.AppendText(tmpBcode & vbCrLf)  'Writes first Bacode and moves to next line for next entry

            SplitContainer1.Panel2.Visible = True
            lblDrumCount.Text = 1

            btnUpdate.Visible = True

            firstDrum = 1
            txtBoxDrumBcode.Clear()
            txtBoxDrumBcode.Focus()

        ElseIf firstDrum > 0 Then


            For i = 1 To txtBoxScanDrum.Lines.Count
                If txtBoxDrumBcode.Text = txtBoxScanDrum.Lines(i - 1) Then
                    lblMessage.Visible = True
                    lblMessage.Text = "This DRUM has already " & vbCrLf & "been scanned " & vbCrLf & "Please check"
                    DelayTM()
                    lblMessage.Visible = False

                    txtBoxDrumBcode.Clear()
                    txtBoxDrumBcode.Focus()
                    Exit Sub
                End If
            Next


            'tmpIdxCount = txtBoxScanDrum.Lines.Count - 1

            'UPDATE RICHTEXT BOX WITH DRUM VALUE
            txtBoxScanDrum.AppendText(tmpBcode & vbCrLf)

            tmpIdxCount = txtBoxScanDrum.Lines.Count

            lblDrumCount.Text = tmpIdxCount - 1




            txtBoxDrumBcode.Clear()
            txtBoxDrumBcode.Focus()

        End If

    End Sub





    '******************************************************************************   START SQL DATBASE ROUTINES  ************************************************************
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
            Me.Cursor = System.Windows.Forms.Cursors.Default
        End Try

    End Sub

    ' ADD PARAMS
    Public Sub LAddParam(Name As String, Value As Object)
        Dim NewParam As New SqlParameter(Name, Value)
        LParams.Add(NewParam)
    End Sub

    ' ERROR CHECKING
    Public Function HasException(Optional Report As Boolean = False) As Boolean
        If String.IsNullOrEmpty(LException) Then Return False
        If Report = True Then MsgBox(LException, MsgBoxStyle.Critical, "Exception:")
        Return True
    End Function
    '***************************************************************************  End of SQL Section ************************************************************************


    Private Sub DelayTM()
        Dim interval As Integer = "3000"  '5sec Delay time
        Dim sw As New Stopwatch
        sw.Start()
        Do While sw.ElapsedMilliseconds < interval
            Application.DoEvents()
        Loop
        sw.Stop()
    End Sub

    Public Sub timeUpdate()   'get current time and date


        Dim tmpDate As DateTime
        tmpDate = DateTime.Now.ToString(New System.Globalization.CultureInfo("en-US"))
        todayTimeDate = Format(tmpDate, "yyyy-MM-dd HH:mm:ss")

    End Sub


    'THIS LOOKS For ENTER key To be pressed Or received via barcode
    Private Sub frmSelectRelease_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown

        If e.KeyCode = Keys.Return Then prgContinue()

    End Sub


    Private Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        btnUpdate.Visible = False

        txtBoxDrumBcode.Clear()
        txtBoxDrumBcode.Refresh()


        txtBoxScanDrum.Clear()
        txtBoxScanDrum.Refresh()
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Me.Close()
        frmHoldRelMethod.btnGradeAB.Enabled = True
        frmHoldRelMethod.btnGradeA.Enabled = True
        frmHoldRelMethod.btnWaste.Enabled = True

        frmHoldRelMethod.btnGradeAB.BackColor = Color.Yellow
        frmHoldRelMethod.btnGradeA.BackColor = Color.YellowGreen
        frmHoldRelMethod.btnWaste.BackColor = Color.Violet

        frmHoldRelMethod.btnChangeSel.Visible = False
        frmHoldRelMethod.btnOK.Visible = False

        frmHoldRelMethod.varRelGrade = ""


        frmHoldRelMethod.Show()

    End Sub

    Private Sub btnUpdate_Click(sender As Object, e As EventArgs) Handles btnUpdate.Click

        Dim updateDrum As String

        timeUpdate()


        Select Case tmpRelGrade
            Case "A"

                For i = 1 To txtBoxScanDrum.Lines.Count - 1
                    updateDrum = txtBoxScanDrum.Lines(i - 1).ToString


                    LExecQuery("Update poytrack2 Set POYDRUMSTATE = 3, POYRELEASENAME = '" & tmpOpName & "', POYHOLDRELEASETM = '" & todayTimeDate & "' " _
                                    & "Where POYBCODEDRUM = '" & updateDrum & "' ")

                Next


            Case "AB"
                Dim tmpDrumNum As String


                For i = 1 To txtBoxScanDrum.Lines.Count - 1
                    updateDrum = txtBoxScanDrum.Lines(i - 1).ToString
                    tmpDrumNum = updateDrum.Substring(12, 2)


                    LExecQuery("Update poytrack2 Set POYDRUMSTATE = 3, POYRELEASENAME = '" & tmpOpName & "', POYHOLDRELEASETM = '" & todayTimeDate & "' , " _
                                & " POYDEFDRUM = '" & tmpDrumNum & "' Where POYBCODEDRUM = '" & updateDrum & "' ")
                Next

            Case "WASTE"
                Dim tmpDrumNum As String

                For i = 1 To txtBoxScanDrum.Lines.Count - 1
                    updateDrum = txtBoxScanDrum.Lines(i - 1).ToString
                    tmpDrumNum = updateDrum.Substring(12, 2)

                    LExecQuery("Update poytrack2 Set POYDRUMSTATE = 3, POYRELEASENAME = '" & tmpOpName & "', POYHOLDRELEASETM = '" & todayTimeDate & "' , " _
                              & "POYDRUMWASTE =  '" & tmpDrumNum & "' Where POYBCODEDRUM = '" & updateDrum & "' ")
                Next

        End Select

        'GO BACK TO RELEASE METHOD SCREEN
        Me.Close()
        frmHoldRelMethod.btnGradeAB.Enabled = True
        frmHoldRelMethod.btnGradeA.Enabled = True
        frmHoldRelMethod.btnWaste.Enabled = True

        frmHoldRelMethod.btnGradeAB.BackColor = Color.Yellow
        frmHoldRelMethod.btnGradeA.BackColor = Color.YellowGreen
        frmHoldRelMethod.btnWaste.BackColor = Color.Violet

        frmHoldRelMethod.btnChangeSel.Visible = False
        frmHoldRelMethod.btnOK.Visible = False

        frmHoldRelMethod.varRelGrade = ""


        frmHoldRelMethod.Show()




    End Sub
End Class