// ReSharper disable All
using MixERP.Net.DbFactory;
using MixERP.Net.Framework;
using MixERP.Net.Framework.Extensions;
using PetaPoco;
using MixERP.Net.Entities.Transactions;
using Npgsql;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
namespace MixERP.Net.Schemas.Transactions.Data
{
    /// <summary>
    /// Prepares, validates, and executes the function "transactions.get_party_transaction_summary(office_id integer, party_id bigint)" on the database.
    /// </summary>
    public class GetPartyTransactionSummaryProcedure : DbAccess, IGetPartyTransactionSummaryRepository
    {
        /// <summary>
        /// The schema of this PostgreSQL function.
        /// </summary>
        public override string _ObjectNamespace => "transactions";
        /// <summary>
        /// The schema unqualified name of this PostgreSQL function.
        /// </summary>
        public override string _ObjectName => "get_party_transaction_summary";
        /// <summary>
        /// Login id of application user accessing this PostgreSQL function.
        /// </summary>
        public long _LoginId { get; set; }
        /// <summary>
        /// User id of application user accessing this table.
        /// </summary>
        public int _UserId { get; set; }
        /// <summary>
        /// The name of the database on which queries are being executed to.
        /// </summary>
        public string _Catalog { get; set; }

        /// <summary>
        /// Maps to "office_id" argument of the function "transactions.get_party_transaction_summary".
        /// </summary>
        public int OfficeId { get; set; }
        /// <summary>
        /// Maps to "party_id" argument of the function "transactions.get_party_transaction_summary".
        /// </summary>
        public long PartyId { get; set; }

        /// <summary>
        /// Prepares, validates, and executes the function "transactions.get_party_transaction_summary(office_id integer, party_id bigint)" on the database.
        /// </summary>
        public GetPartyTransactionSummaryProcedure()
        {
        }

        /// <summary>
        /// Prepares, validates, and executes the function "transactions.get_party_transaction_summary(office_id integer, party_id bigint)" on the database.
        /// </summary>
        /// <param name="officeId">Enter argument value for "office_id" parameter of the function "transactions.get_party_transaction_summary".</param>
        /// <param name="partyId">Enter argument value for "party_id" parameter of the function "transactions.get_party_transaction_summary".</param>
        public GetPartyTransactionSummaryProcedure(int officeId, long partyId)
        {
            this.OfficeId = officeId;
            this.PartyId = partyId;
        }
        /// <summary>
        /// Prepares and executes the function "transactions.get_party_transaction_summary".
        /// </summary>
        /// <exception cref="UnauthorizedException">Thown when the application user does not have sufficient privilege to perform this action.</exception>
        public IEnumerable<DbGetPartyTransactionSummaryResult> Execute()
        {
            if (!this.SkipValidation)
            {
                if (!this.Validated)
                {
                    this.Validate(AccessTypeEnum.Execute, this._LoginId, this._Catalog, false);
                }
                if (!this.HasAccess)
                {
                    Log.Information("Access to the function \"GetPartyTransactionSummaryProcedure\" was denied to the user with Login ID {LoginId}.", this._LoginId);
                    throw new UnauthorizedException("Access is denied.");
                }
            }
            string query = "SELECT * FROM transactions.get_party_transaction_summary(@OfficeId, @PartyId);";

            query = query.ReplaceWholeWord("@OfficeId", "@0::integer");
            query = query.ReplaceWholeWord("@PartyId", "@1::bigint");


            List<object> parameters = new List<object>();
            parameters.Add(this.OfficeId);
            parameters.Add(this.PartyId);

            return Factory.Get<DbGetPartyTransactionSummaryResult>(this._Catalog, query, parameters.ToArray());
        }


    }
}