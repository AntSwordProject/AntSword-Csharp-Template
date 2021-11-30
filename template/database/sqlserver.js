
module.exports = () => ({
    show_databases: {
        _: `###DATABASE###`,
        'action': 'show_databases',
        'conn': '#{newbase64::conn}'
    },
    show_tables: {
        _: `###DATABASE###`,
        'action': 'show_tables',
        'conn': '#{newbase64::conn}',
        'z1': '#{newbase64::db}'
    },
    show_columns: {
        _: `###DATABASE###`,
        'action': 'show_columns',
        'conn': '#{newbase64::conn}',
        'z1': '#{newbase64::db}',
        'z2': '#{newbase64::table}'
    },
    query: {
        _: `###DATABASE###`,
        'action': 'query',
        'conn': '#{newbase64::conn}',
        'z1': '#{newbase64::sql}'
    }
})
