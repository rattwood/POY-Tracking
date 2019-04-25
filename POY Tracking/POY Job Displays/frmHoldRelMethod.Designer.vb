<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmHoldRelMethod
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmHoldRelMethod))
        Me.lblButtons = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtBoxOpName = New System.Windows.Forms.TextBox()
        Me.btnGradeAB = New System.Windows.Forms.Button()
        Me.btnGradeA = New System.Windows.Forms.Button()
        Me.btnWaste = New System.Windows.Forms.Button()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.btnOK = New System.Windows.Forms.Button()
        Me.btnChangeSel = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'lblButtons
        '
        Me.lblButtons.AutoSize = True
        Me.lblButtons.BackColor = System.Drawing.SystemColors.ActiveCaption
        Me.lblButtons.Font = New System.Drawing.Font("Microsoft Sans Serif", 20.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblButtons.ForeColor = System.Drawing.SystemColors.HighlightText
        Me.lblButtons.Location = New System.Drawing.Point(154, 180)
        Me.lblButtons.Name = "lblButtons"
        Me.lblButtons.Size = New System.Drawing.Size(515, 31)
        Me.lblButtons.TabIndex = 0
        Me.lblButtons.Text = "Select how to release DRUMS on Hold"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.SystemColors.ActiveCaption
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 20.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.SystemColors.ControlLightLight
        Me.Label2.Location = New System.Drawing.Point(273, 40)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(291, 31)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "Enter Operator Name"
        '
        'txtBoxOpName
        '
        Me.txtBoxOpName.Font = New System.Drawing.Font("Microsoft Sans Serif", 24.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBoxOpName.Location = New System.Drawing.Point(171, 97)
        Me.txtBoxOpName.Name = "txtBoxOpName"
        Me.txtBoxOpName.Size = New System.Drawing.Size(471, 44)
        Me.txtBoxOpName.TabIndex = 2
        '
        'btnGradeAB
        '
        Me.btnGradeAB.BackColor = System.Drawing.Color.Yellow
        Me.btnGradeAB.Enabled = False
        Me.btnGradeAB.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnGradeAB.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnGradeAB.Location = New System.Drawing.Point(148, 262)
        Me.btnGradeAB.Name = "btnGradeAB"
        Me.btnGradeAB.Size = New System.Drawing.Size(114, 73)
        Me.btnGradeAB.TabIndex = 3
        Me.btnGradeAB.Text = "AB"
        Me.btnGradeAB.UseVisualStyleBackColor = False
        '
        'btnGradeA
        '
        Me.btnGradeA.BackColor = System.Drawing.Color.YellowGreen
        Me.btnGradeA.Enabled = False
        Me.btnGradeA.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnGradeA.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnGradeA.Location = New System.Drawing.Point(349, 262)
        Me.btnGradeA.Name = "btnGradeA"
        Me.btnGradeA.Size = New System.Drawing.Size(114, 73)
        Me.btnGradeA.TabIndex = 4
        Me.btnGradeA.Text = "A"
        Me.btnGradeA.UseVisualStyleBackColor = False
        '
        'btnWaste
        '
        Me.btnWaste.BackColor = System.Drawing.Color.Violet
        Me.btnWaste.Enabled = False
        Me.btnWaste.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnWaste.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnWaste.Location = New System.Drawing.Point(577, 262)
        Me.btnWaste.Name = "btnWaste"
        Me.btnWaste.Size = New System.Drawing.Size(114, 73)
        Me.btnWaste.TabIndex = 5
        Me.btnWaste.Text = "Waste"
        Me.btnWaste.UseVisualStyleBackColor = False
        '
        'btnCancel
        '
        Me.btnCancel.BackColor = System.Drawing.Color.OrangeRed
        Me.btnCancel.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCancel.Location = New System.Drawing.Point(12, 381)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(147, 57)
        Me.btnCancel.TabIndex = 6
        Me.btnCancel.Text = "Cancel"
        Me.btnCancel.UseVisualStyleBackColor = False
        '
        'btnOK
        '
        Me.btnOK.BackColor = System.Drawing.Color.LimeGreen
        Me.btnOK.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnOK.Location = New System.Drawing.Point(641, 381)
        Me.btnOK.Name = "btnOK"
        Me.btnOK.Size = New System.Drawing.Size(147, 57)
        Me.btnOK.TabIndex = 7
        Me.btnOK.Text = "OK"
        Me.btnOK.UseVisualStyleBackColor = False
        Me.btnOK.Visible = False
        '
        'btnChangeSel
        '
        Me.btnChangeSel.BackColor = System.Drawing.Color.Snow
        Me.btnChangeSel.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnChangeSel.Location = New System.Drawing.Point(328, 381)
        Me.btnChangeSel.Name = "btnChangeSel"
        Me.btnChangeSel.Size = New System.Drawing.Size(147, 57)
        Me.btnChangeSel.TabIndex = 8
        Me.btnChangeSel.Text = "ReSet"
        Me.btnChangeSel.UseVisualStyleBackColor = False
        Me.btnChangeSel.Visible = False
        '
        'frmHoldRelMethod
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.ActiveCaption
        Me.ClientSize = New System.Drawing.Size(800, 450)
        Me.Controls.Add(Me.btnChangeSel)
        Me.Controls.Add(Me.btnOK)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.btnWaste)
        Me.Controls.Add(Me.btnGradeA)
        Me.Controls.Add(Me.btnGradeAB)
        Me.Controls.Add(Me.txtBoxOpName)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.lblButtons)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmHoldRelMethod"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "frmHoldRelMethod"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents lblButtons As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents txtBoxOpName As TextBox
    Friend WithEvents btnGradeAB As Button
    Friend WithEvents btnGradeA As Button
    Friend WithEvents btnWaste As Button
    Friend WithEvents btnCancel As Button
    Friend WithEvents btnOK As Button
    Friend WithEvents btnChangeSel As Button
End Class
