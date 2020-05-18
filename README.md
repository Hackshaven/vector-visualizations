Click here to launch an interactive container: [![Binder](https://mybinder.org/badge_logo.svg)](https://mybinder.org/v2/gh/Hackshaven/vector-visualizations/master?filepath=Tornado%20NetCDF.ipynb)

<img src="unity-vfx.png">

# GPU-Based Vector Field Visualizations using 3D Textures

Goal is to create information-rich visualizations for geo-science that have meaningful and useful interpretations for scientists and educators. To accomplish this goal, new features in Unity3D are leveraged to utilize GPU power for complex particle systems, with a focus on 3D vector force fields (VFF). By creating a volume having UVW vectors at each point, we can inject particles into this volume and watch the dynamics. An analogy is a pin-ball machine, whose mechanisms supply the dynamics that effect ordinary balls. 

The architecture converts [NetCDF](https://www.unidata.ucar.edu/software/netcdf/) & [GRIB](https://en.wikipedia.org/wiki/GRIB) datasets into force vector fields for rendering as [3DTextures](https://docs.unity3d.com/Manual/class-Texture3D.html) in [Unity Visual Effect Graphs](https://unity.com/visual-effect-graph).

Issues to be resolved:

* Mapping dataset dimensions and values to 3D texture dimensionds and values
* Specifying slices of the dataset dimensions to the server
* Managing a library of datasets to be visualized
* Bringing the real-world interpretation of these dimensions into Unity
* Varying the VFX graph to show different aspects of the UVW field
* Rigging camera to best visualize the UVW rendering

# Documentation

- In addition to the python, the Unity VFX sample project should be cloned from https://github.com/Unity-Technologies/VisualEffectGraph-Samples
- The included unity package should installed in that project.
- More to follow...
