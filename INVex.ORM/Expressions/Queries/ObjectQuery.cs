using INVex.ORM.DataBase.Common;
using INVex.ORM.Expressions.Base;
using INVex.ORM.Expressions.Modify;
using INVex.ORM.Expressions.Objects;
using INVex.ORM.Expressions.Queries.Base;
using INVex.ORM.Holders;
using INVex.ORM.Objects.Attributes.Base;
using INVex.ORM.Objects.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace INVex.ORM.Expressions.Queries
{
    public class ObjectQuery : BaseQuery
    {
        public override Dictionary<string, object> QueryParameters { get; set; }
        public override string QueryString { get; protected set; }
        public override bool NeedTransaction { get; set; } = false;

        public int Top { get; set; }
        public OrderType OrderType { get; set; } = OrderType.DESC;


        public List<IPathElement> ReturnedAttributes { get; set; } = new List<IPathElement>();
        public IPathElement OrderBy { get; set; }


        private IAttributeModel orderByAttribute;
        private IObjectInstance ownerInstance;
        private ICriteria _criteria = null;
        public ICriteria Criteria
        {
            get
            {
                return this._criteria;
            }
            set
            {
                this._criteria = value;
                this._criteria.SetOwner(this.ownerInstance);
            }
        }


        public ObjectQuery(string modelName)
        {
            this.ownerInstance = ObjectModelsHolder.Current.Holder.CreateInstance(modelName);
        }

        protected virtual void ParseQuery()
        {
            if (this.ReturnedAttributes.Count > 0)
            {
                foreach (IPathElement path in this.ReturnedAttributes)
                {
                    this.ownerInstance.AddRequiredAttribute(path.ProcessElement(this.ownerInstance));
                }
            }

            #region WHERE
            StringBuilder whereStmnt = new StringBuilder();

            // First, parse where statment
            if(this._criteria != null)
            {
                SqlComputedString computedString = this._criteria.ToSql();

                if (!string.IsNullOrEmpty(computedString.Query))
                {
                    this.QueryParameters = computedString.SqlParameters;

                    whereStmnt.AppendFormat("WHERE {0}", computedString.Query);
                }
            }

            #endregion

            StringBuilder showedColumns = new StringBuilder();

            #region Joins
            StringBuilder joinStmnt = new StringBuilder();

            int tableNum = 0;

            foreach (IPathElement pathEl in this.ownerInstance.RequiredAttributes)
            {
                if(pathEl is IAttributePath)
                {
                    foreach (IAttributeStep step in ((IAttributePath)pathEl).Steps)
                    {
                        if (step.Attribute is IReferenceAttribute)
                        {
                            IReferenceAttribute refAttr = (IReferenceAttribute)step.Attribute;

                            tableNum++;

                            string joinTableName = "t" + tableNum;

                            joinStmnt.AppendFormat("LEFT JOIN {0} AS {1} ON {2}={3}",
                                refAttr.Field.Reference.Table.FullName,
                                joinTableName,
                                joinTableName + "." + step.Attribute.Mapping.ColumnName,
                                refAttr.Field.Reference.PrimaryKey.Mapping.ColumnName
                                );
                            showedColumns.AppendFormat("[{0}].[{1}],", joinTableName, step.Attribute.Owner.PrimaryKey.Mapping.ColumnName);
                        }
                    }
                }                
                else
                {
                    showedColumns.AppendFormat("[{0}],", pathEl.ProcessElement(this.ownerInstance).Mapping.ColumnName);
                }
            }

            #endregion

            #region Order by

            StringBuilder orderStmnt = new StringBuilder();

            if(this.OrderBy != null)
            {
                this.orderByAttribute = this.OrderBy.ProcessElement(this.ownerInstance);

                string orderType = string.Empty;

                switch (this.OrderType)
                {
                    case OrderType.DESC:
                        orderType = "DESC";
                        break;
                    case OrderType.ASC:
                        orderType = "ASC";
                        break;
                }

                orderStmnt.AppendFormat("ORDER BY {0}.{1} {2}", this.orderByAttribute.Owner.Table.FullName, this.orderByAttribute.Mapping.ColumnName, orderType);
            }

            #endregion

            #region Top

            string topStmnt = this.Top == 0 ? string.Empty : string.Format("TOP {0}", this.Top);

            #endregion

            #region SELECT

            StringBuilder selectStmnt = new StringBuilder();
            selectStmnt.AppendFormat("SELECT {0} {1} FROM {2} {3} {4} {5}",
                topStmnt,
                showedColumns.ToString().TrimEnd(','),
                this.ownerInstance.Table.FullName,
                joinStmnt.ToString(),
                whereStmnt.ToString(),
                orderStmnt.ToString()
                );

            this.QueryString = selectStmnt.ToString();

            #endregion
        }

        public new List<IObjectInstance> Execute()
        {
            this.ParseQuery();

            List<IObjectInstance> objectInstances = new List<IObjectInstance>();
            ExecutionResult executionResult = (ExecutionResult)base.Execute();

            foreach (RowResult row in executionResult.Rows)
            {
                IObjectInstance objectInstance = ObjectModelsHolder.Current.Holder.CreateInstance(this.ownerInstance.Model.Name);

                foreach (string columnName in row.Keys)
                {
                    objectInstance.Attributes[objectInstance.GetAttributeByMappingColumn(columnName).Name].Field.ForceSet(row[columnName]);
                }

                objectInstances.Add(objectInstance);
            }


            return objectInstances;
        }

        public List<T> Execute<T>() where T : IObjectInstance
        {
            this.ParseQuery();

            List<T> objectInstances = new List<T>();
            ExecutionResult executionResult = (ExecutionResult)base.Execute();

            foreach (RowResult row in executionResult.Rows)
            {
                T objectInstance = (T)ObjectModelsHolder.Current.Holder.CreateInstance(this.ownerInstance.Model.Name);

                foreach (string columnName in row.Keys)
                {
                    objectInstance.Attributes[objectInstance.GetAttributeByMappingColumn(columnName).Name].Field.ForceSet(row[columnName]);
                }

                objectInstances.Add(objectInstance);
            }


            return objectInstances;
        }
    }
}
