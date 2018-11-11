<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmToolEntry
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
        Me.Label1 = New System.Windows.Forms.Label()
        Me.btnChangeDrum = New System.Windows.Forms.Button()
        Me.btnChangeSteps = New System.Windows.Forms.Button()
        Me.btnChangeTrace = New System.Windows.Forms.Button()
        Me.txtTraceNum = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.lblError = New System.Windows.Forms.Label()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.btnClear = New System.Windows.Forms.Button()
        Me.lblProduct = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.lblMerge = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.lblDate = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.lblPalSize = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.lblDrums = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.lblComplete = New System.Windows.Forms.Label()
        Me.lblTraceComplete = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(356, 26)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(133, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "PALLET EDITING TOOLS"
        '
        'btnChangeDrum
        '
        Me.btnChangeDrum.BackColor = System.Drawing.Color.LightGray
        Me.btnChangeDrum.Enabled = False
        Me.btnChangeDrum.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnChangeDrum.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnChangeDrum.Location = New System.Drawing.Point(103, 247)
        Me.btnChangeDrum.Name = "btnChangeDrum"
        Me.btnChangeDrum.Size = New System.Drawing.Size(120, 46)
        Me.btnChangeDrum.TabIndex = 1
        Me.btnChangeDrum.Text = "Change DRUMS"
        Me.btnChangeDrum.UseVisualStyleBackColor = False
        '
        'btnChangeSteps
        '
        Me.btnChangeSteps.BackColor = System.Drawing.Color.LightGray
        Me.btnChangeSteps.Enabled = False
        Me.btnChangeSteps.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnChangeSteps.Location = New System.Drawing.Point(365, 247)
        Me.btnChangeSteps.Name = "btnChangeSteps"
        Me.btnChangeSteps.Size = New System.Drawing.Size(120, 46)
        Me.btnChangeSteps.TabIndex = 2
        Me.btnChangeSteps.Text = "Change STEPS"
        Me.btnChangeSteps.UseVisualStyleBackColor = False
        '
        'btnChangeTrace
        '
        Me.btnChangeTrace.BackColor = System.Drawing.Color.LightGray
        Me.btnChangeTrace.Enabled = False
        Me.btnChangeTrace.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnChangeTrace.Location = New System.Drawing.Point(622, 247)
        Me.btnChangeTrace.Name = "btnChangeTrace"
        Me.btnChangeTrace.Size = New System.Drawing.Size(120, 46)
        Me.btnChangeTrace.TabIndex = 3
        Me.btnChangeTrace.Text = "Change TRACE"
        Me.btnChangeTrace.UseVisualStyleBackColor = False
        '
        'txtTraceNum
        '
        Me.txtTraceNum.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTraceNum.Location = New System.Drawing.Point(487, 64)
        Me.txtTraceNum.Name = "txtTraceNum"
        Me.txtTraceNum.Size = New System.Drawing.Size(134, 26)
        Me.txtTraceNum.TabIndex = 4
        Me.txtTraceNum.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(240, 65)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(227, 25)
        Me.Label2.TabIndex = 5
        Me.Label2.Text = "Scan Trace Number:"
        '
        'lblError
        '
        Me.lblError.AutoSize = True
        Me.lblError.BackColor = System.Drawing.Color.Red
        Me.lblError.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.lblError.Font = New System.Drawing.Font("Microsoft Sans Serif", 26.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblError.Location = New System.Drawing.Point(45, 106)
        Me.lblError.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.lblError.Name = "lblError"
        Me.lblError.Size = New System.Drawing.Size(126, 39)
        Me.lblError.TabIndex = 334
        Me.lblError.Text = "Label3"
        Me.lblError.Visible = False
        '
        'btnCancel
        '
        Me.btnCancel.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.btnCancel.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCancel.Location = New System.Drawing.Point(12, 424)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(120, 46)
        Me.btnCancel.TabIndex = 333
        Me.btnCancel.Text = "Cancel"
        Me.btnCancel.UseVisualStyleBackColor = False
        '
        'btnClear
        '
        Me.btnClear.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnClear.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClear.Location = New System.Drawing.Point(180, 424)
        Me.btnClear.Name = "btnClear"
        Me.btnClear.Size = New System.Drawing.Size(120, 46)
        Me.btnClear.TabIndex = 335
        Me.btnClear.Text = "Clear"
        Me.btnClear.UseVisualStyleBackColor = False
        '
        'lblProduct
        '
        Me.lblProduct.AutoSize = True
        Me.lblProduct.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblProduct.ForeColor = System.Drawing.Color.DodgerBlue
        Me.lblProduct.Location = New System.Drawing.Point(336, 107)
        Me.lblProduct.Name = "lblProduct"
        Me.lblProduct.Size = New System.Drawing.Size(45, 13)
        Me.lblProduct.TabIndex = 337
        Me.lblProduct.Text = "Label4"
        Me.lblProduct.Visible = False
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(260, 132)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(70, 13)
        Me.Label5.TabIndex = 338
        Me.Label5.Text = "Merge No.:"
        Me.Label5.Visible = False
        '
        'lblMerge
        '
        Me.lblMerge.AutoSize = True
        Me.lblMerge.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMerge.ForeColor = System.Drawing.Color.DodgerBlue
        Me.lblMerge.Location = New System.Drawing.Point(336, 133)
        Me.lblMerge.Name = "lblMerge"
        Me.lblMerge.Size = New System.Drawing.Size(45, 13)
        Me.lblMerge.TabIndex = 339
        Me.lblMerge.Text = "Label6"
        Me.lblMerge.Visible = False
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(242, 158)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(88, 13)
        Me.Label7.TabIndex = 340
        Me.Label7.Text = "Packing Date:"
        Me.Label7.Visible = False
        '
        'lblDate
        '
        Me.lblDate.AutoSize = True
        Me.lblDate.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDate.ForeColor = System.Drawing.Color.DodgerBlue
        Me.lblDate.Location = New System.Drawing.Point(336, 158)
        Me.lblDate.Name = "lblDate"
        Me.lblDate.Size = New System.Drawing.Size(45, 13)
        Me.lblDate.TabIndex = 341
        Me.lblDate.Text = "Label8"
        Me.lblDate.Visible = False
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(485, 107)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(71, 13)
        Me.Label9.TabIndex = 342
        Me.Label9.Text = "Pallet Size:"
        Me.Label9.Visible = False
        '
        'lblPalSize
        '
        Me.lblPalSize.AutoSize = True
        Me.lblPalSize.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPalSize.ForeColor = System.Drawing.Color.DodgerBlue
        Me.lblPalSize.Location = New System.Drawing.Point(562, 107)
        Me.lblPalSize.Name = "lblPalSize"
        Me.lblPalSize.Size = New System.Drawing.Size(52, 13)
        Me.lblPalSize.TabIndex = 343
        Me.lblPalSize.Text = "Label10"
        Me.lblPalSize.Visible = False
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.Location = New System.Drawing.Point(454, 133)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(102, 13)
        Me.Label11.TabIndex = 344
        Me.Label11.Text = "Drums On Pallet:"
        Me.Label11.Visible = False
        '
        'lblDrums
        '
        Me.lblDrums.AutoSize = True
        Me.lblDrums.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDrums.ForeColor = System.Drawing.Color.DodgerBlue
        Me.lblDrums.Location = New System.Drawing.Point(562, 133)
        Me.lblDrums.Name = "lblDrums"
        Me.lblDrums.Size = New System.Drawing.Size(52, 13)
        Me.lblDrums.TabIndex = 345
        Me.lblDrums.Text = "Label12"
        Me.lblDrums.Visible = False
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(242, 106)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(88, 13)
        Me.Label3.TabIndex = 346
        Me.Label3.Text = "Product Kind :"
        Me.Label3.Visible = False
        '
        'lblComplete
        '
        Me.lblComplete.AutoSize = True
        Me.lblComplete.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblComplete.ForeColor = System.Drawing.Color.Tomato
        Me.lblComplete.Location = New System.Drawing.Point(384, 296)
        Me.lblComplete.Name = "lblComplete"
        Me.lblComplete.Size = New System.Drawing.Size(66, 13)
        Me.lblComplete.TabIndex = 347
        Me.lblComplete.Text = "Completed"
        Me.lblComplete.Visible = False
        '
        'lblTraceComplete
        '
        Me.lblTraceComplete.AutoSize = True
        Me.lblTraceComplete.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTraceComplete.ForeColor = System.Drawing.Color.Tomato
        Me.lblTraceComplete.Location = New System.Drawing.Point(648, 296)
        Me.lblTraceComplete.Name = "lblTraceComplete"
        Me.lblTraceComplete.Size = New System.Drawing.Size(66, 13)
        Me.lblTraceComplete.TabIndex = 348
        Me.lblTraceComplete.Text = "Completed"
        Me.lblTraceComplete.Visible = False
        '
        'frmToolEntry
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(850, 482)
        Me.Controls.Add(Me.lblTraceComplete)
        Me.Controls.Add(Me.lblComplete)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.lblDrums)
        Me.Controls.Add(Me.Label11)
        Me.Controls.Add(Me.lblPalSize)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.lblDate)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.lblMerge)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.lblProduct)
        Me.Controls.Add(Me.btnClear)
        Me.Controls.Add(Me.lblError)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.txtTraceNum)
        Me.Controls.Add(Me.btnChangeTrace)
        Me.Controls.Add(Me.btnChangeSteps)
        Me.Controls.Add(Me.btnChangeDrum)
        Me.Controls.Add(Me.Label1)
        Me.Name = "frmToolEntry"
        Me.Text = "Pallet Edit Tools"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Label1 As Label
    Friend WithEvents btnChangeDrum As Button
    Friend WithEvents btnChangeSteps As Button
    Friend WithEvents btnChangeTrace As Button
    Friend WithEvents txtTraceNum As TextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents lblError As Label
    Friend WithEvents btnCancel As Button
    Friend WithEvents btnClear As Button
    Friend WithEvents lblProduct As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents lblMerge As Label
    Friend WithEvents Label7 As Label
    Friend WithEvents lblDate As Label
    Friend WithEvents Label9 As Label
    Friend WithEvents lblPalSize As Label
    Friend WithEvents Label11 As Label
    Friend WithEvents lblDrums As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents lblComplete As Label
    Friend WithEvents lblTraceComplete As Label
End Class
