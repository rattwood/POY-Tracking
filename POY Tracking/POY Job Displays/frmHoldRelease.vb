'Imports System.Data.SqlClient
'Imports System.ComponentModel
'Imports System.Text


'Public Class frmHoldRelease


'    'Private SQL As New SQLConn
'    Private writeerrorLog As New writeError

'    '---------------------------------------    SETTING UP LOCAL INSTANCE FOR SQL LINK FOR DATAGRID TO SYNC CORRECTLY WITH SQL -------------------------------------
'    Public LConn As New SqlConnection(My.Settings.SQLConn) 'This need to be changed in Project/Propertie/Settings
'    ' Public LConn As New SqlConnection("Server=192.168.1.211,1433;Database=Toraydb;User ID=sa;Password=tecknose4260")

'    Private LCmd As SqlCommand

'    'SQL CONNECTORS
'    Public LDA As SqlDataAdapter
'    Public LDS As DataSet
'    Public LDT As DataTable
'    Public LCB As SqlCommandBuilder

'    Public LRecordCount As Integer
'    Private LException As String
'    ' SQL QUERY PARAMETERS
'    Public LParams As New List(Of SqlParameter)

'    'VARIABLES
'    Dim tmpOPName As String
'    Dim tmpDrumNo As String
'    Dim stateGo As Integer = 0


'    Private Sub frmHoldRelease_Load(sender As Object, e As EventArgs) Handles MyBase.Load



'    End Sub

'    Private Sub txtBoxOpName_TextChanged(sender As Object, e As EventArgs)
'        lblScanDrum.Visible = True
'        txtBoxDrumNo.Visible = True
'        Me.KeyPreview = True  'Allows us to look for advace character from barcode
'    End Sub

'    Private Sub checkDrum()
'        tmpDrumNo = txtBoxDrumNo.Text

'        LExecQuery("Select poybcodedrum from POYTRACK2 Where poybcodedrum = '" & tmpDrumNo & "'And poydrumstate = 4")
'        If LRecordCount > 0 Then
'            gbLogIn.Visible = False
'            gbButtons.Visible = True
'            gbProductInfo.Visible = True
'            gbSpare.Visible = False
'            gbDrums.Visible = True


'        Else
'            lblMessage.Text = " Drum is not on Hold or does not exist in the system  "
'            lblMessage.Visible = True
'            DelayTM()
'            lblMessage.Visible = False
'            txtBoxDrumNo.Clear()
'            txtBoxDrumNo.Focus()

'        End If

'    End Sub


'    '******************************************************************************   START SQL DATBASE ROUTINES  ************************************************************
'    Public Sub LExecQuery(Query As String)
'        ' RESET QUERY STATISTCIS
'        LRecordCount = 0
'        LException = ""


'        If LConn.State = ConnectionState.Open Then LConn.Close()
'        Try

'            'OPEN SQL DATABSE CONNECTION
'            LConn.Open()

'            'CREATE SQL COMMAND
'            LCmd = New SqlCommand(Query, LConn)

'            'LOAD PARAMETER INTO SQL COMMAND
'            LParams.ForEach(Sub(p) LCmd.Parameters.Add(p))

'            'CLEAR PARAMETER LIST
'            LParams.Clear()

'            'EXECUTE COMMAND AND FILL DATASET
'            LDS = New DataSet
'            LDT = New DataTable
'            LDA = New SqlDataAdapter(LCmd)

'            LRecordCount = LDA.Fill(LDS)

'        Catch ex As Exception

'            LException = "ExecQuery Error: " & vbNewLine & ex.Message
'            MsgBox(LException)
'            Me.Cursor = System.Windows.Forms.Cursors.Default
'        End Try

'    End Sub

'    ' ADD PARAMS
'    Public Sub LAddParam(Name As String, Value As Object)
'        Dim NewParam As New SqlParameter(Name, Value)
'        LParams.Add(NewParam)
'    End Sub

'    ' ERROR CHECKING
'    Public Function HasException(Optional Report As Boolean = False) As Boolean
'        If String.IsNullOrEmpty(LException) Then Return False
'        If Report = True Then MsgBox(LException, MsgBoxStyle.Critical, "Exception:")
'        Return True
'    End Function
'    '***************************************************************************  End of SQL Section ************************************************************************
'    Private Sub DelayTM()
'        Dim interval As Integer = "5000"  '5sec Delay time
'        Dim sw As New Stopwatch
'        sw.Start()
'        Do While sw.ElapsedMilliseconds < interval
'            Application.DoEvents()
'        Loop
'        sw.Stop()
'    End Sub




'    Private Sub btnCancel_Click(sender As Object, e As EventArgs)
'        Me.Close()
'        frmSortJobDisplay.Show()
'    End Sub

'    Private Sub btnClear_Click(sender As Object, e As EventArgs)
'        txtBoxDrumNo.Clear()
'        txtBoxOpName.Clear()
'        txtBoxOpName.Focus()

'    End Sub

'    'THIS LOOKS FOR ENTER key to be pressed or received via barcode
'    Private Sub frmJobEntry_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown

'        If e.KeyCode = Keys.Return Then

'            If Not stateGo Then
'                checkDrum()


'            Else

'                'prgContinue()

'            End If
'        End If

'    End Sub

'    Private Sub btnCancelInput_Click(sender As Object, e As EventArgs)
'        gbLogIn.Visible = True
'        gbButtons.Visible = False
'        gbProductInfo.Visible = False
'        gbSpare.Visible = False
'        gbDrums.Visible = False

'        txtBoxDrums.Clear()
'        txtBoxDrumNo.Clear()
'        txtBoxOpName.Clear()
'        txtBoxOpName.Focus()
'    End Sub
'End Class