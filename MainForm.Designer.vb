Partial Class MainForm
    ''' <summary>
    ''' Required designer variable.
    ''' </summary>
    Private components As System.ComponentModel.IContainer = Nothing

    ''' <summary>
    ''' Clean up any resources being used.
    ''' </summary>
    ''' <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso (components IsNot Nothing) Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

#Region "Windows Form Designer generated code"

    ''' <summary>
    ''' Required method for Designer support - do not modify
    ''' the contents of this method with the code editor.
    ''' </summary>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(MainForm))
        Me.lblTimerDisplay = New System.Windows.Forms.Label()
        Me.cmbAction = New System.Windows.Forms.ComboBox()
        Me.lblAction = New System.Windows.Forms.Label()
        Me.btnOK = New System.Windows.Forms.Button()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.mainTimer = New System.Windows.Forms.Timer(Me.components)
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'lblTimerDisplay
        '
        Me.lblTimerDisplay.BackColor = System.Drawing.Color.Black
        Me.lblTimerDisplay.Font = New System.Drawing.Font("Arial", 16.0!, System.Drawing.FontStyle.Bold)
        Me.lblTimerDisplay.ForeColor = System.Drawing.Color.Green
        Me.lblTimerDisplay.Location = New System.Drawing.Point(226, 268)
        Me.lblTimerDisplay.Name = "lblTimerDisplay"
        Me.lblTimerDisplay.Size = New System.Drawing.Size(140, 30)
        Me.lblTimerDisplay.TabIndex = 0
        Me.lblTimerDisplay.Text = "00:00:00"
        Me.lblTimerDisplay.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.lblTimerDisplay.Visible = False
        '
        'cmbAction
        '
        Me.cmbAction.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbAction.FormattingEnabled = True
        Me.cmbAction.Items.AddRange(New Object() {"Shutdown", "Restart"})
        Me.cmbAction.Location = New System.Drawing.Point(171, 44)
        Me.cmbAction.Name = "cmbAction"
        Me.cmbAction.Size = New System.Drawing.Size(250, 21)
        Me.cmbAction.TabIndex = 1
        '
        'lblAction
        '
        Me.lblAction.AutoSize = True
        Me.lblAction.Location = New System.Drawing.Point(255, 18)
        Me.lblAction.Name = "lblAction"
        Me.lblAction.Size = New System.Drawing.Size(91, 13)
        Me.lblAction.TabIndex = 0
        Me.lblAction.Text = "Action to Perform:"
        '
        'btnOK
        '
        Me.btnOK.Location = New System.Drawing.Point(200, 320)
        Me.btnOK.Name = "btnOK"
        Me.btnOK.Size = New System.Drawing.Size(80, 30)
        Me.btnOK.TabIndex = 2
        Me.btnOK.Text = "Start"
        Me.btnOK.UseVisualStyleBackColor = True
        '
        'btnCancel
        '
        Me.btnCancel.Location = New System.Drawing.Point(300, 320)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(80, 30)
        Me.btnCancel.TabIndex = 3
        Me.btnCancel.Text = "Stop"
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'mainTimer
        '
        Me.mainTimer.Interval = 1000
        '
        'PictureBox1
        '
        Me.PictureBox1.ImageLocation = "C:\Users\agus\Documents\TEKRA\PUTRIMALLU\logo.png"
        Me.PictureBox1.Location = New System.Drawing.Point(216, 81)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(150, 170)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.PictureBox1.TabIndex = 4
        Me.PictureBox1.TabStop = False
        '
        'MainForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(584, 361)
        Me.Controls.Add(Me.PictureBox1)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.btnOK)
        Me.Controls.Add(Me.lblAction)
        Me.Controls.Add(Me.cmbAction)
        Me.Controls.Add(Me.lblTimerDisplay)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.Name = "MainForm"
        Me.Text = "PutriMallu V3 - Simple Timer"
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

#End Region

    Friend WithEvents lblTimerDisplay As System.Windows.Forms.Label
    ' Friend WithEvents btnBrowseFolder As System.Windows.Forms.Button ' REMOVED
    ' Friend WithEvents txtFolderPath As System.Windows.Forms.TextBox ' REMOVED
    ' Friend WithEvents lblFolderToCompress As System.Windows.Forms.Label ' REMOVED
    ' Friend WithEvents txtPassword As System.Windows.Forms.TextBox ' REMOVED
    ' Friend WithEvents lblPassword As System.Windows.Forms.Label ' REMOVED
    Friend WithEvents cmbAction As System.Windows.Forms.ComboBox
    Friend WithEvents lblAction As System.Windows.Forms.Label
    Friend WithEvents btnOK As System.Windows.Forms.Button
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents mainTimer As System.Windows.Forms.Timer
    Friend WithEvents PictureBox1 As PictureBox
End Class