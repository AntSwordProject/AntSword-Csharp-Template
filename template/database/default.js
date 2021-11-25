//
// 默认代码模板
//
// @params
// :encode  SHELL编码
// :conn    数据库连接字符串
// :sql     执行SQL语句
// :db      数据库名
// :table   表名

module.exports = () => ({
  show_databases: {
    _: `###DATABASE###`,
    'action': 'show_databases',
    'conn': '#{conn}'
  },
  show_tables: {
    _: `###DATABASE###`,
    'action': 'show_tables',
    'conn': '#{conn}',
    'z1': '#{db}'
  },
  show_columns: {
    _: `###DATABASE###`,
    'action': 'show_columns',
    'conn': '#{conn}',
    'db': '#{db}',
    'z2': '#{table}'
  },
  query: {
    _: `###DATABASE###`,
    'action': 'query',
    'conn': '#{conn}',
    'z1': '#{sql}'
  }
})
