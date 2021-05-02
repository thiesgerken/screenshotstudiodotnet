cd ..
paraffin -update Main.wxs
del Main.wxs
move Main.PARAFFIN Main.wxs

paraffin -update Main.de.wxs
del Main.de.wxs
move Main.de.PARAFFIN Main.de.wxs

cd Scripts