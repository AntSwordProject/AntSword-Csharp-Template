//
// 文件管理模板
//

module.exports = (arg1, arg2, arg3) => ({
  dir: {
    _: '###Dir###',
    'path': '#{newbase64::path}'
  },

  delete: {
    _: '###Delete###',
    'path': '#{newbase64::path}'
  },

  create_file: {
    _: '###CreateFile###',
    'path': '#{newbase64::path}',
    'content': '#{newbase64::content}'
  },

  read_file: {
    _: '###ReadFile###',
    'path': '#{newbase64::path}'
  },
  copy: {
    _: '###Copy###',
    'path': '#{newbase64::path}',
    'target': '#{newbase64::target}'
  },

  download_file: {
    _: '###DownloadFile###',
    'path': '#{newbase64::path}'
  },

  upload_file: {
    _: '###UploadFile###',
    'path': '#{newbase64::path}',
    'content': '#{newb64buffer::content}'
  },

  rename: {
    _: '###Rename###',
    'path': '#{newbase64::path}',
    'name': '#{newbase64::name}'
  },

  retime: {
    _: '###Retime###',
    'path': '#{newbase64::path}',
    'time': '#{newbase64::time}'
  },

  chmod: {
    _: '###Chmod###',
    'path': '#{newbase64::path}',
    'mode': '#{newbase64::mode}',
  },

  mkdir: {
    _: '###Mkdir###',
    'path': '#{newbase64::path}'
  },

  wget: {
    _: '###Wget###',
    'url': '#{newbase64::url}',
    'path': '#{newbase64::path}'
  }
})