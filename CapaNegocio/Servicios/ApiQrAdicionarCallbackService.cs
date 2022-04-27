using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using CapaDatos.Data;
using CapaDatos.Response;
using CapaNegocio.Servicios;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using WebIntiApi.Models;
using CapaDatos.Request;

namespace CapaNegocio.Servicios
{
    public class ApiQrAdicionarCallbackService: IApiQrAdicionarCallbackService
    {
		private readonly AppSettings _appSettings;
		private readonly AplicacionDbContext2 _context;
		public ApiQrAdicionarCallbackService(IOptions<AppSettings> appSettings, AplicacionDbContext2 context)
		{
			_appSettings = appSettings.Value;
			_context = context;
		}
		public clsQrAdicionarCallbackResponse adicionarQr(clsQrAdicionarCallbackRequest model)
		{
			//
			SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection();
			SqlCommand cmd = conn.CreateCommand();
			conn.Open();
			cmd.CommandType = System.Data.CommandType.StoredProcedure;
			cmd.CommandText = "Api_QrAdicionarCallback";
			cmd.Parameters.Add("@qrIdGenerado", System.Data.SqlDbType.VarChar, 20).Value = model.qrIdGenerado;
			cmd.Parameters.Add("@calCorrelationId", System.Data.SqlDbType.VarChar, 50).Value = model.calCorrelationId;
			cmd.Parameters.Add("@calServiceCode", System.Data.SqlDbType.VarChar, 6).Value = model.calServiceCode;
			cmd.Parameters.Add("@calBussinesCode", System.Data.SqlDbType.VarChar, 4).Value = model.calBussinesCode;
			cmd.Parameters.Add("@calIdQrAch", System.Data.SqlDbType.VarChar, 45).Value = model.calIdQrAch;

			if (model.calEif == null) model.calEif = "";
			cmd.Parameters.Add("@calEif", System.Data.SqlDbType.Char, 5).Value = model.calEif;
			if (model.calAccount == null) model.calAccount = "";
			cmd.Parameters.Add("@calAccount", System.Data.SqlDbType.VarChar, 20).Value = model.calAccount;
			cmd.Parameters.Add("@calAmount", System.Data.SqlDbType.Decimal).Value = model.calAmount;
			cmd.Parameters.Add("@calCurrency", System.Data.SqlDbType.Char, 3).Value = model.calCurrency;
			cmd.Parameters.Add("@calGloss", System.Data.SqlDbType.NVarChar, 60).Value = model.calGloss;
			cmd.Parameters.Add("@calReceiverAccount", System.Data.SqlDbType.NVarChar, 45).Value = model.calReceiverAccount;
			cmd.Parameters.Add("@calReceiverName", System.Data.SqlDbType.NVarChar, 150).Value = model.calReceiverName;
			cmd.Parameters.Add("@calReceiverDocument", System.Data.SqlDbType.VarChar, 10).Value = model.calReceiverDocument;
			cmd.Parameters.Add("@calReceiverBank", System.Data.SqlDbType.VarChar, 20).Value = model.calReceiverBank;
			if (model.calExpirationDate == null) model.calExpirationDate = "";
			cmd.Parameters.Add("@calExpirationDate", System.Data.SqlDbType.VarChar, 16).Value = model.calExpirationDate;
			cmd.Parameters.Add("@calResponseCode", System.Data.SqlDbType.Char, 3).Value = model.calResponseCode;
			cmd.Parameters.Add("@calStatus", System.Data.SqlDbType.Char, 1).Value = model.calStatus;
			if (model.calRequest == null) model.calRequest = "";
			cmd.Parameters.Add("@calRequest", System.Data.SqlDbType.NVarChar, 250).Value = model.calRequest;
			cmd.Parameters.Add("@calRequestDate", System.Data.SqlDbType.DateTime).Value = model.calRequestDate;
			if (model.calResponse == null) model.calResponse = "";
			cmd.Parameters.Add("@calResponse", System.Data.SqlDbType.NVarChar, 250).Value = model.calResponse;
			cmd.Parameters.Add("@calResponseDate", System.Data.SqlDbType.DateTime).Value = model.calResponseDate;
			if (model.calResponseArch == null) model.calResponseArch = "";
			cmd.Parameters.Add("@calResponseArch", System.Data.SqlDbType.NVarChar, 250).Value = model.calResponseArch;
			cmd.Parameters.Add("@calResponseAchDate", System.Data.SqlDbType.DateTime).Value = model.calResponseAchDate;

			cmd.Parameters.Add("@calDescription", System.Data.SqlDbType.NVarChar, 250).Value = model.calDescription;
			cmd.Parameters.Add("@calGenerateType", System.Data.SqlDbType.Int).Value = model.calGenerateType;
			cmd.Parameters.Add("@calVersion", System.Data.SqlDbType.VarChar, 10).Value = model.calVersion;
			cmd.Parameters.Add("@calSingleUse", System.Data.SqlDbType.Char, 1).Value = model.calSingleUse;
			cmd.Parameters.Add("@calOperationNumber", System.Data.SqlDbType.VarChar, 10).Value = model.calOperationNumber;
			if (model.calEnableBlack == null) model.calEnableBlack = "";
			cmd.Parameters.Add("@calEnableBlack", System.Data.SqlDbType.VarChar, 10).Value = model.calEnableBlack;

			if (model.calCity == null) model.calCity = "";
			cmd.Parameters.Add("@calCity", System.Data.SqlDbType.VarChar, 45).Value = model.calCity;
			if (model.calBrachOffice == null) model.calBrachOffice = "";
			cmd.Parameters.Add("@calBrachOffice", System.Data.SqlDbType.VarChar, 150).Value = model.calBrachOffice;
			if (model.calTeller == null) model.calTeller = "";
			cmd.Parameters.Add("@calTeller", System.Data.SqlDbType.VarChar, 45).Value = model.calTeller;
			if (model.calPhoneNumber == null) model.calPhoneNumber = "";
			cmd.Parameters.Add("@calPhoneNumber", System.Data.SqlDbType.VarChar, 15).Value = model.calPhoneNumber;
			if (model.IdCorrelation == null) model.IdCorrelation = "";
			cmd.Parameters.Add("@IdCorrelation", System.Data.SqlDbType.VarChar, 50).Value = model.IdCorrelation;

			var adicionar = new clsQrAdicionarCallbackResponse();
			var reader = cmd.ExecuteReader();

			// Respuesta Procedimiento almacenado defecto
			int qrId = -10;
			while (reader.Read())
			{
				qrId = Convert.ToInt32(reader["qrId"]);
			}

			adicionar.qrAdicionado = qrId;

			conn.Close();

			return adicionar;
		}
	}
}
