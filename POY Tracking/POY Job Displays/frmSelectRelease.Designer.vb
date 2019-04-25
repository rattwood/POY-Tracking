<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmSelectRelease
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.lblOpName = New System.Windows.Forms.Label()
        Me.lblProdNum = New System.Windows.Forms.Label()
        Me.lblProdName = New System.Windows.Forms.Label()
        Me.lblRelGrade = New System.Windows.Forms.Label()
        Me.lblMessage = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txtBoxDrumBcode = New System.Windows.Forms.TextBox()
        Me.btnClear = New System.Windows.Forms.Button()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.lblDrumCount = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.btnUpdate = New System.Windows.Forms.Button()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtBoxScanDrum = New System.Windows.Forms.TextBox()
        Me.lblTextBox = New System.Windows.Forms.Label()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.SuspendLayout()
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer1.Name = "SplitContainer1"
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.BackColor = System.Drawing.SystemColors.ActiveCaption
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblOpName)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblProdNum)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblProdName)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblRelGrade)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblMessage)
        Me.SplitContainer1.Panel1.Controls.Add(Me.Label5)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtBoxDrumBcode)
        Me.SplitContainer1.Panel1.Controls.Add(Me.btnClear)
        Me.SplitContainer1.Panel1.Controls.Add(Me.btnCancel)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblDrumCount)
        Me.SplitContainer1.Panel1.Controls.Add(Me.Label4)
        Me.SplitContainer1.Panel1.Controls.Add(Me.btnUpdate)
        Me.SplitContainer1.Panel1.Controls.Add(Me.Label6)
        Me.SplitContainer1.Panel1.Controls.Add(Me.Label2)
        Me.SplitContainer1.Panel1.Controls.Add(Me.Label3)
        Me.SplitContainer1.Panel1.Controls.Add(Me.Label1)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.BackColor = System.Drawing.Color.AliceBlue
        Me.SplitContainer1.Panel2.Controls.Add(Me.txtBoxScanDrum)
        Me.SplitContainer1.Panel2.Controls.Add(Me.lblTextBox)
        Me.SplitContainer1.Size = New System.Drawing.Size(1085, 770)
        Me.SplitContainer1.SplitterDistance = 844
        Me.SplitContainer1.TabIndex = 0
        '
        'lblOpName
        '
        Me.lblOpName.AutoSize = True
        Me.lblOpName.Font = New System.Drawing.Font("Microsoft Sans Serif", 24.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblOpName.ForeColor = System.Drawing.SystemColors.ButtonHighlight
        Me.lblOpName.Location = New System.Drawing.Point(288, 206)
        Me.lblOpName.Name = "lblOpName"
        Me.lblOpName.Size = New System.Drawing.Size(41, 37)
        Me.lblOpName.TabIndex = 11
        Me.lblOpName.Text = "D"
        '
        'lblProdNum
        '
        Me.lblProdNum.AutoSize = True
        Me.lblProdNum.Font = New System.Drawing.Font("Microsoft Sans Serif", 24.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblProdNum.ForeColor = System.Drawing.SystemColors.ButtonHighlight
        Me.lblProdNum.Location = New System.Drawing.Point(288, 73)
        Me.lblProdNum.Name = "lblProdNum"
        Me.lblProdNum.Size = New System.Drawing.Size(39, 37)
        Me.lblProdNum.TabIndex = 9
        Me.lblProdNum.Text = "B"
        Me.lblProdNum.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblProdName
        '
        Me.lblProdName.AutoSize = True
        Me.lblProdName.Font = New System.Drawing.Font("Microsoft Sans Serif", 24.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblProdName.ForeColor = System.Drawing.SystemColors.ButtonHighlight
        Me.lblProdName.Location = New System.Drawing.Point(291, 135)
        Me.lblProdName.Name = "lblProdName"
        Me.lblProdName.Size = New System.Drawing.Size(41, 37)
        Me.lblProdName.TabIndex = 8
        Me.lblProdName.Text = "C"
        '
        'lblRelGrade
        '
        Me.lblRelGrade.AutoSize = True
        Me.lblRelGrade.Font = New System.Drawing.Font("Microsoft Sans Serif", 24.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblRelGrade.ForeColor = System.Drawing.SystemColors.ButtonHighlight
        Me.lblRelGrade.Location = New System.Drawing.Point(288, 9)
        Me.lblRelGrade.Name = "lblRelGrade"
        Me.lblRelGrade.Size = New System.Drawing.Size(40, 37)
        Me.lblRelGrade.TabIndex = 3
        Me.lblRelGrade.Text = "A"
        Me.lblRelGrade.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblMessage
        '
        Me.lblMessage.AutoSize = True
        Me.lblMessage.Font = New System.Drawing.Font("Microsoft Sans Serif", 26.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMessage.ForeColor = System.Drawing.Color.Red
        Me.lblMessage.Location = New System.Drawing.Point(94, 448)
        Me.lblMessage.Name = "lblMessage"
        Me.lblMessage.Size = New System.Drawing.Size(126, 39)
        Me.lblMessage.TabIndex = 49
        Me.lblMessage.Text = "Label7"
        Me.lblMessage.Visible = False
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(439, 311)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(111, 31)
        Me.Label5.TabIndex = 50
        Me.Label5.Text = "DRUM #"
        '
        'txtBoxDrumBcode
        '
        Me.txtBoxDrumBcode.Font = New System.Drawing.Font("Microsoft Sans Serif", 36.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBoxDrumBcode.Location = New System.Drawing.Point(279, 352)
        Me.txtBoxDrumBcode.Name = "txtBoxDrumBcode"
        Me.txtBoxDrumBcode.Size = New System.Drawing.Size(424, 62)
        Me.txtBoxDrumBcode.TabIndex = 0
        Me.txtBoxDrumBcode.Text = "88888888888888"
        Me.txtBoxDrumBcode.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.txtBoxDrumBcode.WordWrap = False
        '
        'btnClear
        '
        Me.btnClear.BackColor = System.Drawing.Color.White
        Me.btnClear.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClear.Location = New System.Drawing.Point(310, 679)
        Me.btnClear.Name = "btnClear"
        Me.btnClear.Size = New System.Drawing.Size(217, 86)
        Me.btnClear.TabIndex = 2
        Me.btnClear.Text = "Clear"
        Me.btnClear.UseVisualStyleBackColor = False
        '
        'btnCancel
        '
        Me.btnCancel.BackColor = System.Drawing.Color.Red
        Me.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnCancel.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCancel.Location = New System.Drawing.Point(611, 679)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(217, 86)
        Me.btnCancel.TabIndex = 3
        Me.btnCancel.Text = "Cancel"
        Me.btnCancel.UseVisualStyleBackColor = False
        '
        'lblDrumCount
        '
        Me.lblDrumCount.AutoSize = True
        Me.lblDrumCount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblDrumCount.Font = New System.Drawing.Font("Microsoft Sans Serif", 36.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDrumCount.ForeColor = System.Drawing.Color.LightCyan
        Me.lblDrumCount.Location = New System.Drawing.Point(98, 357)
        Me.lblDrumCount.Name = "lblDrumCount"
        Me.lblDrumCount.Size = New System.Drawing.Size(54, 57)
        Me.lblDrumCount.TabIndex = 7
        Me.lblDrumCount.Text = "1"
        Me.lblDrumCount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(28, 311)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(216, 31)
        Me.Label4.TabIndex = 48
        Me.Label4.Text = "DRUMS Scanned"
        '
        'btnUpdate
        '
        Me.btnUpdate.BackColor = System.Drawing.Color.Lime
        Me.btnUpdate.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnUpdate.Location = New System.Drawing.Point(9, 679)
        Me.btnUpdate.Name = "btnUpdate"
        Me.btnUpdate.Size = New System.Drawing.Size(217, 86)
        Me.btnUpdate.TabIndex = 10
        Me.btnUpdate.Text = "Update"
        Me.btnUpdate.UseVisualStyleBackColor = False
        Me.btnUpdate.Visible = False
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 24.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.SystemColors.Info
        Me.Label6.Location = New System.Drawing.Point(12, 209)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(154, 39)
        Me.Label6.TabIndex = 47
        Me.Label6.Text = "Operator"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 24.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.SystemColors.Info
        Me.Label2.Location = New System.Drawing.Point(12, 138)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(247, 39)
        Me.Label2.TabIndex = 46
        Me.Label2.Text = "Product Name."
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 24.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.SystemColors.Info
        Me.Label3.Location = New System.Drawing.Point(12, 73)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(201, 39)
        Me.Label3.TabIndex = 45
        Me.Label3.Text = "Product No."
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 24.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.SystemColors.Info
        Me.Label1.Location = New System.Drawing.Point(12, 10)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(206, 39)
        Me.Label1.TabIndex = 44
        Me.Label1.Text = "Release as  "
        '
        'txtBoxScanDrum
        '
        Me.txtBoxScanDrum.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBoxScanDrum.Location = New System.Drawing.Point(3, 30)
        Me.txtBoxScanDrum.Multiline = True
        Me.txtBoxScanDrum.Name = "txtBoxScanDrum"
        Me.txtBoxScanDrum.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtBoxScanDrum.Size = New System.Drawing.Size(229, 735)
        Me.txtBoxScanDrum.TabIndex = 51
        Me.txtBoxScanDrum.Text = "65574190496010"
        Me.txtBoxScanDrum.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'lblTextBox
        '
        Me.lblTextBox.AutoSize = True
        Me.lblTextBox.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTextBox.Location = New System.Drawing.Point(49, 7)
        Me.lblTextBox.Name = "lblTextBox"
        Me.lblTextBox.Size = New System.Drawing.Size(137, 20)
        Me.lblTextBox.TabIndex = 51
        Me.lblTextBox.Text = "Scanned Drums"
        '
        'frmSelectRelease
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1085, 770)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "frmSelectRelease"
        Me.Text = "Release Selected"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.PerformLayout()
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.Panel2.PerformLayout()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents SplitContainer1 As SplitContainer
    Friend WithEvents Label1 As Label
    Friend WithEvents lblRelGrade As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents lblTextBox As Label
    Friend WithEvents btnUpdate As Button
    Friend WithEvents Label6 As Label
    Friend WithEvents btnClear As Button
    Friend WithEvents btnCancel As Button
    Friend WithEvents lblOpName As Label
    Friend WithEvents lblProdNum As Label
    Friend WithEvents lblProdName As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents lblDrumCount As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents txtBoxDrumBcode As TextBox
    Friend WithEvents lblMessage As Label
    Friend WithEvents txtBoxScanDrum As TextBox
End Class
