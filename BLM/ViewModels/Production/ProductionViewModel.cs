﻿using BLM.Models;
using BLM.ViewModels.Production.Forms;
using BLM.ViewModels.Shipments.Forms;
using Caliburn.Micro;
using System;
using System.Data;

namespace BLM.ViewModels.Production
{
    internal class ProductionViewModel : Screen
    {
        private readonly IWindowManager windowManager = new WindowManager();
        private object _productionGridSelectedItem;

        private DataTable _productionGridSource;

        private string _selectedStatus;

        private string _txtSearch;

        public object productionGridSelectedItem
        {
            get { return _productionGridSelectedItem; }
            set { _productionGridSelectedItem = value; }
        }

        public DataTable productionGridSource
        {
            get { return _productionGridSource; }
            set { _productionGridSource = value; }
        }
        private string _txtProductName;

        public string txtProductName
        {
            get { return _txtProductName; }
            set { _txtProductName = value; }
        }

        public string txtSearch
        {
            get { return _txtSearch; }
            set
            {
                _txtSearch = value;
                DataView dv = new DataView(_productionGridSource);
                dv.RowFilter = "Origin LIKE '%" + _txtSearch + "%' OR Destination LIKE '%" + _txtSearch + "%'";
                _productionGridSource = dv.ToTable();
                NotifyOfPropertyChange(null);
            }
        }
        public void btnCreate()
        {
            windowManager.ShowWindow(new NewProductionViewModel(), null, null);
        }

        public void btnExport()
        {
        }

        public void btnFinished()
        {
            _productionGridSource = Connection.dbTable(
                 "SELECT prod_id as 'ID'," +
                 "prod_name as 'NAME'," +
                 "prod_theoretical_yield as 'THEORETICAL'," +
    //             "prod_item_name as 'ITEM'," +
    //             "prod_category as 'CATEGORY'," +
    //             "prod_qty as 'QUANTITY'," +
    //              "prod_received_weight as 'WEIGHT'," +
    //              "prod_size as 'SIZE'," +
    //              "prod_unit as 'UNIT'," +
                 "prod_status as 'STATUS'," +
                 "prod_rfid as 'RFID' " +
                 "from flc.production where prod_status = 'Finished' group by prod_rfid");
            NotifyOfPropertyChange(null);
            _selectedStatus = "Finished";
        }
        public void btnPending()
        {
             _productionGridSource = Connection.dbTable(
                 "SELECT prod_id as 'ID'," +
                 "prod_name as 'NAME'," +
                 "prod_theoretical_yield as 'THEORETICAL'," +
    //             "prod_item_name as 'ITEM'," +
    //             "prod_category as 'CATEGORY'," +
    //             "prod_qty as 'QUANTITY'," +
    //              "prod_received_weight as 'WEIGHT'," +
    //              "prod_size as 'SIZE'," +
    //              "prod_unit as 'UNIT'," +
                 "prod_status as 'STATUS'," +
                 "prod_rfid as 'RFID' " +
                 "from flc.production where prod_status = 'Pending' group by prod_rfid");

            NotifyOfPropertyChange(null);
            _selectedStatus = "Pending";
        }

        public void btnProcessing()
        {
            _productionGridSource = Connection.dbTable(
                 "SELECT prod_id as 'ID'," +
                 "prod_name as 'NAME'," +
                 "prod_theoretical_yield as 'THEORETICAL'," +
     //            "prod_item_name as 'ITEM'," +
     //            "prod_category as 'CATEGORY'," +
     //            "prod_qty as 'QUANTITY'," +
     //             "prod_received_weight as 'WEIGHT'," +
     //             "prod_size as 'SIZE'," +
     //             "prod_unit as 'UNIT'," +
                 "prod_status as 'STATUS'," +
                 "prod_rfid as 'RFID' " +
                 "from flc.production where prod_status = 'Processing' group by prod_rfid");
            NotifyOfPropertyChange(null);
            _selectedStatus = "Processing";
        }

        public void btnRefresh()
        {
            _productionGridSource = Connection.dbTable(
                 "SELECT prod_id as 'ID'," +
                 "prod_name as 'NAME'," +
                 "prod_theoretical_yield as 'THEORETICAL'," +
                 "prod_item_name as 'ITEM'," +
                 "prod_category as 'CATEGORY'," +
                 "prod_qty as 'QUANTITY'," +
                  "prod_received_weight as 'WEIGHT'," +
                  "prod_size as 'SIZE'," +
                  "prod_unit as 'UNIT'," +
                 "prod_status as 'STATUS'," +
                 "prod_rfid as 'RFID' " +
                 "from flc.production");
            NotifyOfPropertyChange(() => productionGridSource);
            _txtSearch = string.Empty;
        }

        public void showItem()
        {
            try
            {
                DataRowView dataRowView = (DataRowView)_productionGridSelectedItem;
                // windowManager.ShowWindow(new EditProductionViewModel(Convert.ToInt32(dataRowView.Row[0])), null, null);
            }
            catch
            {
            }
        }


        private string _txtTheoreticalYield;

        public string txtTheoreticalYield
        {
            get { return _txtTheoreticalYield; }
            set { _txtTheoreticalYield = value; }
        }
        private string _txtStatus;

        public string txtStatus
        {
            get { return _txtStatus; }
            set { _txtStatus = value; }
        }
            
        private string _txtRFID;

        public string txtRFID
        {
            get { return _txtRFID; }
            set { _txtRFID = value; }
        }


        public void print()
        {
            try
            {
                DataRowView dataRowView = (DataRowView)_productionGridSelectedItem;
                _txtProductName = dataRowView.Row[1].ToString();
         //       NotifyOfPropertyChange(() => txtProductName);
             _txtTheoreticalYield = dataRowView.Row[2].ToString();
        //        NotifyOfPropertyChange(() => txtTheoreticalYield);
                _txtStatus = dataRowView.Row[9].ToString();
         //       NotifyOfPropertyChange(() => txtStatus);
                _txtRFID = dataRowView.Row[10].ToString();
                NotifyOfPropertyChange(null);
            }
            catch
            {

            }
        }
        protected override void OnActivate()
        {
            _productionGridSource = Connection.dbTable(
                "SELECT prod_id as 'ID'," +
                "prod_name as 'NAME'," +
                "prod_theoretical_yield as 'THEORETICAL'," +
                "prod_item_name as 'ITEM'," +
                "prod_category as 'CATEGORY'," +
                "prod_qty as 'QUANTITY'," +
                 "prod_received_weight as 'WEIGHT'," +
                 "prod_size as 'SIZE'," +
                 "prod_unit as 'UNIT'," +
                "prod_status as 'STATUS', " +
                "prod_rfid as 'RFID' " +
                "from flc.production");
            NotifyOfPropertyChange(()=>productionGridSource);
            base.OnActivate();
        }
    }
}