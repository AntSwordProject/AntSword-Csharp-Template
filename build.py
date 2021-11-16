# coding:UTF-8
# run in python3
# code by yzddMr6
import os
import sys
import base64
import time
import shutil
import subprocess
import platform
import re

# csc路径
cscpath = r'C:\Windows\Microsoft.NET\Framework\v2.0.50727\csc.exe'

pathsep = os.pathsep
distDir = "./dist/"
tempDir = "./temp/"

if os.path.exists(distDir):
    shutil.rmtree(distDir)

if os.path.exists(tempDir):
    shutil.rmtree(tempDir)

if not os.path.exists(tempDir):
    os.mkdir(tempDir)

shutil.copytree("./template/", distDir)

for root, dirs, files in os.walk('.'):
    for f in files:
        if f.endswith(".cs"):
            print('------------------------------------------------------------')
            spath = os.path.join(root, f)
            print("before: "+spath)
            with open(spath, "r", encoding="UTF-8") as sf:
                source = sf.read()
                dist = re.sub(r"public\ class\ (.*)",
                              "public class Run", source)  # 统一替换类名为Run

            path = os.path.join(tempDir, f).replace("/", "\\")
            with open(path, "w", encoding="UTF-8") as df:
                df.write(dist)
            print("after: "+path)

            targetdll = path.replace('.cs', '.dll')

            cmd = '"{cscpath}" /optimize+ /target:library /out:{targetdll} {path} '.format(
                cscpath=cscpath,
                targetdll=targetdll,
                path=path,
            )
            print(cmd)
            p = subprocess.Popen(
                cmd,
                stdout=subprocess.PIPE,
                stderr=subprocess.PIPE,
                shell=True
            )
            out, err = p.communicate()
            try:
                print(err.decode())
            except:
                print(str(err))
            if os.path.exists(targetdll):
                with open(targetdll, 'rb') as dll:
                    content = dll.read()
                res = str(base64.b64encode(content), "UTF-8")

                packname = f.split("_")[0]
                if len(f.split("_")) == 2:
                    funcname = f.split("_")[1].split(".")[0]
                    if packname == "BASE":
                        distFile = "base.js"
                    elif packname == "CMD":
                        distFile = "command.js"
                    elif packname == "FM":
                        distFile = "filemanager.js"
                elif len(f.split("_")) == 3:
                    distFile = "./database/"+f.split("_")[1]+".js"
                    funcname = f.split("_")[2]

                dispath = os.path.join(distDir, distFile)
                with open(dispath, encoding="UTF-8") as disp:
                    result = disp.read().replace('###'+funcname+'###', res)
                with open(dispath, mode="w", encoding="UTF-8") as disp:
                    disp.write(result)
            else:
                print("error")
