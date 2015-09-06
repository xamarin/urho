../Source/./cmake_ios.sh ../Urho3D_iOS -DURHO3D_ANGELSCRIPT=0 -DURHO3D_TOOLS=0
xcodebuild ARCHS=arm64 ONLY_ACTIVE_ARCH=NO -target Urho3D -configuration Release