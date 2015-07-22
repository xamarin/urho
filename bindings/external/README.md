The classes in this directory are taken from OpenTK, and have the following changes:

	* Quaternion: moved the field "w" from the end to the
          beginning, to stay compatible with the C++ implementation.
          Longer term, since we want to share Quaternion with the
          implementation shipping in our libraries, we will have to change the C++ code to match this.

	* NAmespace: moved From "OpenTK" to "Urho"