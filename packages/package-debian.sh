BUILD_DIR=$(pwd)/../AvalonStudio/AvalonStudio
PACK_DIR=$(pwd)/deb
BUILD_VERSION=$(git describe --tags)
TARG_DIR=$PACK_DIR/avalon-studio_$BUILD_VERSION/opt/vitalelement/avalonstudio/bin

rm -rf $TARG_DIR
rm -rf $BUILD_DIR/bin/Release/netcoreapp2.0/debian.8-x64/publish
pushd $BUILD_DIR 
dotnet publish -c Release -r debian.8-x64 -f netcoreapp2.0
popd
mkdir -p $TARG_DIR
cp -rv $BUILD_DIR/bin/Release/netcoreapp2.0/debian.8-x64/publish/. $TARG_DIR
pwd
cp -rv deb/DEBIAN $PACK_DIR/avalon-studio_$BUILD_VERSION/
cp -rv deb/rootfs/. $PACK_DIR/avalon-studio_$BUILD_VERSION/
sed -i -e "s/{VERSION}/$BUILD_VERSION/g" $PACK_DIR/avalon-studio_$BUILD_VERSION/DEBIAN/control 
chmod +x $TARG_DIR/native/unix/clang-format

mkdir -p $PACK_DIR/avalon-studio_$BUILD_VERSION/usr/share/pixmaps/
cp $BUILD_DIR/icon.png $PACK_DIR/avalon-studio_$BUILD_VERSION/usr/share/pixmaps/avalon-studio.png
dpkg-deb --build $PACK_DIR/avalon-studio_$BUILD_VERSION

