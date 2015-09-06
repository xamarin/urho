../Source/./cmake_macosx.sh ../Urho3D_Mac
xcodebuild ARCHS=i386 ONLY_ACTIVE_ARCH=NO -target Urho3D -configuration Release
mv lib/libUrho3D.a lib/32libUrho3D.a
xcodebuild ARCHS=x86_64 ONLY_ACTIVE_ARCH=NO -target Urho3D -configuration Release
mv lib/libUrho3D.a lib/64libUrho3D.a
lipo -create lib/32libUrho3D.a lib/64libUrho3D.a -output lib/libUrho3D.a