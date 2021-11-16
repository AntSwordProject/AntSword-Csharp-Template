//
// 命令执行模板
//

module.exports = (arg1, arg2, arg3) => ({
  exec: {
    _: '###Exec###',
    'bin': "#{newbase64::bin}",
    'cmd': "#{newbase64::cmd}",
    'env': "#{newbase64::env}"
  },
  listcmd: {
    _: '###Listcmd###',
    'binarr': '#{newbase64::binarr}'
  }
})
