namespace Vista.UserControls.Dashboard
{
    partial class Dashboard
    {
        /// <summary> 
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de componentes

        /// <summary> 
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            dgvVentas = new DataGridView();
            chartRanking = new System.Windows.Forms.DataVisualization.Charting.Chart();
            dgvRanking = new DataGridView();
            cmbFiltroPeriodo = new ComboBox();
            label1 = new Label();
            lblTotalFacturado = new Label();
            chartVentas = new System.Windows.Forms.DataVisualization.Charting.Chart();
            ((System.ComponentModel.ISupportInitialize)dgvVentas).BeginInit();
            ((System.ComponentModel.ISupportInitialize)chartRanking).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvRanking).BeginInit();
            ((System.ComponentModel.ISupportInitialize)chartVentas).BeginInit();
            SuspendLayout();
            // 
            // dgvVentas
            // 
            dgvVentas.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvVentas.Location = new Point(23, 63);
            dgvVentas.Name = "dgvVentas";
            dgvVentas.Size = new Size(378, 284);
            dgvVentas.TabIndex = 1;
            // 
            // chartRanking
            // 
            chartArea1.Name = "ChartArea1";
            chartRanking.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            chartRanking.Legends.Add(legend1);
            chartRanking.Location = new Point(421, 424);
            chartRanking.Name = "chartRanking";
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Bar;
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            chartRanking.Series.Add(series1);
            chartRanking.Size = new Size(629, 247);
            chartRanking.TabIndex = 3;
            chartRanking.Text = "chart2";
            // 
            // dgvRanking
            // 
            dgvRanking.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvRanking.Location = new Point(23, 424);
            dgvRanking.Name = "dgvRanking";
            dgvRanking.Size = new Size(378, 247);
            dgvRanking.TabIndex = 4;
            // 
            // cmbFiltroPeriodo
            // 
            cmbFiltroPeriodo.FormattingEnabled = true;
            cmbFiltroPeriodo.Location = new Point(280, 26);
            cmbFiltroPeriodo.Name = "cmbFiltroPeriodo";
            cmbFiltroPeriodo.Size = new Size(121, 23);
            cmbFiltroPeriodo.TabIndex = 5;
            cmbFiltroPeriodo.SelectedIndexChanged += cmbFiltroPeriodo_SelectedIndexChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(234, 29);
            label1.Name = "label1";
            label1.Size = new Size(40, 15);
            label1.TabIndex = 6;
            label1.Text = "Filtrar:";
            // 
            // lblTotalFacturado
            // 
            lblTotalFacturado.AutoSize = true;
            lblTotalFacturado.Location = new Point(26, 27);
            lblTotalFacturado.Name = "lblTotalFacturado";
            lblTotalFacturado.Size = new Size(92, 15);
            lblTotalFacturado.TabIndex = 7;
            lblTotalFacturado.Text = "Total Facturado:";
            // 
            // chartVentas
            // 
            chartArea2.Name = "ChartArea1";
            chartVentas.ChartAreas.Add(chartArea2);
            legend2.Name = "Legend1";
            chartVentas.Legends.Add(legend2);
            chartVentas.Location = new Point(421, 26);
            chartVentas.Name = "chartVentas";
            series2.ChartArea = "ChartArea1";
            series2.Legend = "Legend1";
            series2.Name = "Series1";
            chartVentas.Series.Add(series2);
            chartVentas.Size = new Size(629, 321);
            chartVentas.TabIndex = 8;
            chartVentas.Text = "chart1";
            // 
            // Dashboard
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(chartVentas);
            Controls.Add(lblTotalFacturado);
            Controls.Add(label1);
            Controls.Add(cmbFiltroPeriodo);
            Controls.Add(dgvRanking);
            Controls.Add(chartRanking);
            Controls.Add(dgvVentas);
            Name = "Dashboard";
            Size = new Size(1280, 720);
            ((System.ComponentModel.ISupportInitialize)dgvVentas).EndInit();
            ((System.ComponentModel.ISupportInitialize)chartRanking).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvRanking).EndInit();
            ((System.ComponentModel.ISupportInitialize)chartVentas).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private DataGridView dgvVentas;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartRanking;
        private DataGridView dgvRanking;
        private ComboBox cmbFiltroPeriodo;
        private Label label1;
        private Label lblTotalFacturado;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartVentas;
    }
}
