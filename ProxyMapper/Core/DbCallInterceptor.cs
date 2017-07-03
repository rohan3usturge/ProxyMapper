using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Reflection;
using Castle.Core.Internal;
using Castle.DynamicProxy;
using ProxyMapper.Attributes;
using ProxyMapper.Enums;
using ProxyMapper.Validation;

namespace ProxyMapper.Core
{
    internal class DbCallInterceptor : IInterceptor
    {
        private readonly string _connectionString;

        private readonly IDataProcessor _dataProcessor;

        private readonly IValidatorChain<CallInfo> _validatorChain;


        public DbCallInterceptor(string connectionString, IDataProcessor dataProcessor)
        {
            this._connectionString = connectionString;
            this._dataProcessor = dataProcessor;
            this._validatorChain = new DefaultValidatorChain<CallInfo>(new IValidator<CallInfo>[]
            {
                new CallInfoValidator()
            });
        }

        public void Intercept(IInvocation invocation)
        {
            SqlConnection conn = null;
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            try
            {
                MethodInfo methodInfo = invocation.Method;

                //Attributes-Info
                CallInfo callInfo = methodInfo.GetCustomAttribute<CallInfo>();
                this._validatorChain.Validate(callInfo);
                string queryString = callInfo.QueryString;
                CallType callType = callInfo.CallType;

                //Return Types
                ReturnTypeInfo returnTypeInfo = new ReturnTypeInfo(methodInfo.ReturnType);

                //Get Connections
                conn = this.GetConnection();

                //Create Command
                SqlCommand sqlCommand = conn.CreateCommand();
                sqlCommand.CommandType = callType.Equals(CallType.Procedure)
                    ? CommandType.StoredProcedure
                    : CommandType.Text;
                sqlCommand.CommandText = queryString;

                //Add Parameters to Command
                ParameterInfo[] parameterInfos = methodInfo.GetParameters();
                //Arguments
                object[] arguments = invocation.Arguments;

                int i = 0;
                foreach (object argument in arguments)
                {
                    sqlCommand.Parameters.AddWithValue(parameterInfos[i++].Name, argument);
                }

                //Call DB
                conn.Open();
                if (returnTypeInfo.IsVoid)
                {
                    sqlCommand.ExecuteNonQuery();
                }
                else
                {
                    SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                    IList list = this._dataProcessor.ProcessDataFromDb(sqlDataReader, returnTypeInfo);
                    invocation.ReturnValue = null;
                    if (returnTypeInfo.IsCollection)
                    {
                        invocation.ReturnValue = list;
                    }
                    else
                    {
                        if (!list.IsNullOrEmpty())
                        {
                            invocation.ReturnValue = list[0];
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //this._logger.LogError($"Db Call Processing Failed for - {callInfo}");
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                conn?.Close();
                stopwatch.Stop();
                //this._logger.LogInformation($"Db Call Processing completed in {stopwatch.ElapsedMilliseconds} ms for - {callInfo}");
            }
        }


        private SqlConnection GetConnection()
        {
            return new SqlConnection(this._connectionString);
        }
    }
}