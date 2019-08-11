using UnityEngine;

public static class EaseAnimationCurves
{

    public static readonly AnimationCurve Zero = AnimationCurve.Constant (0, 1, 0);

    public static readonly AnimationCurve One = AnimationCurve.Constant (0,1,1);

    public static readonly AnimationCurve Linear = AnimationCurve.Linear (0,0,1,1);

    public static readonly AnimationCurve QuadOut = new AnimationCurve
    {
        keys = new Keyframe[]
            {
            new Keyframe { time = 0f, value = 0f, inTangent = 1.928571f, outTangent = 1.928571f, inWeight = 0f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.07142857f, value = 0.1377551f, inTangent = 1.857143f, outTangent = 1.857143f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.1428571f, value = 0.2653061f, inTangent = 1.714286f, outTangent = 1.714286f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.2142857f, value = 0.3826531f, inTangent = 1.571428f, outTangent = 1.571428f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.2857143f, value = 0.4897959f, inTangent = 1.428571f, outTangent = 1.428571f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.3571429f, value = 0.5867347f, inTangent = 1.285714f, outTangent = 1.285714f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.4285714f, value = 0.6734694f, inTangent = 1.142857f, outTangent = 1.142857f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.5f, value = 0.75f, inTangent = 1f, outTangent = 1f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.5714286f, value = 0.8163266f, inTangent = 0.8571429f, outTangent = 0.8571429f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.6428571f, value = 0.872449f, inTangent = 0.7142859f, outTangent = 0.7142859f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.7142857f, value = 0.9183674f, inTangent = 0.5714287f, outTangent = 0.5714287f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.7857143f, value = 0.9540817f, inTangent = 0.4285713f, outTangent = 0.4285713f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.8571429f, value = 0.9795918f, inTangent = 0.2857141f, outTangent = 0.2857141f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.9285714f, value = 0.994898f, inTangent = 0.1428571f, outTangent = 0.1428571f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 1f, value = 1f, inTangent = 0.07142852f, outTangent = 0.07142852f, inWeight = 0.3333333f, outWeight = 0f, },
            }
    };

    public static readonly AnimationCurve QuadIn = new AnimationCurve
    {
        keys = new Keyframe[]
            {
            new Keyframe { time = 0f, value = 0f, inTangent = 0.07142857f, outTangent = 0.07142857f, inWeight = 0f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.07142857f, value = 0.005102041f, inTangent = 0.1428571f, outTangent = 0.1428571f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.1428571f, value = 0.02040816f, inTangent = 0.2857143f, outTangent = 0.2857143f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.2142857f, value = 0.04591837f, inTangent = 0.4285714f, outTangent = 0.4285714f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.2857143f, value = 0.08163266f, inTangent = 0.5714285f, outTangent = 0.5714285f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.3571429f, value = 0.127551f, inTangent = 0.7142857f, outTangent = 0.7142857f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.4285714f, value = 0.1836735f, inTangent = 0.8571429f, outTangent = 0.8571429f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.5f, value = 0.25f, inTangent = 1f, outTangent = 1f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.5714286f, value = 0.3265306f, inTangent = 1.142857f, outTangent = 1.142857f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.6428571f, value = 0.4132653f, inTangent = 1.285714f, outTangent = 1.285714f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.7142857f, value = 0.5102041f, inTangent = 1.428571f, outTangent = 1.428571f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.7857143f, value = 0.6173469f, inTangent = 1.571429f, outTangent = 1.571429f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.8571429f, value = 0.7346939f, inTangent = 1.714286f, outTangent = 1.714286f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.9285714f, value = 0.8622448f, inTangent = 1.857143f, outTangent = 1.857143f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 1f, value = 1f, inTangent = 1.928571f, outTangent = 1.928571f, inWeight = 0.3333333f, outWeight = 0f, },
            }
    };

    public static readonly AnimationCurve QuadInOut = new AnimationCurve
    {
        keys = new Keyframe[]
            {
            new Keyframe { time = 0f, value = 0f, inTangent = 0.1428571f, outTangent = 0.1428571f, inWeight = 0f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.07142857f, value = 0.01020408f, inTangent = 0.2857143f, outTangent = 0.2857143f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.1428571f, value = 0.04081633f, inTangent = 0.5714286f, outTangent = 0.5714286f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.2142857f, value = 0.09183674f, inTangent = 0.8571429f, outTangent = 0.8571429f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.2857143f, value = 0.1632653f, inTangent = 1.142857f, outTangent = 1.142857f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.3571429f, value = 0.255102f, inTangent = 1.428571f, outTangent = 1.428571f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.4285714f, value = 0.3673469f, inTangent = 1.714286f, outTangent = 1.714286f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.5f, value = 0.5f, inTangent = 1.857143f, outTangent = 1.857143f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.5714286f, value = 0.6326531f, inTangent = 1.714286f, outTangent = 1.714286f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.6428571f, value = 0.744898f, inTangent = 1.428571f, outTangent = 1.428571f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.7142857f, value = 0.8367347f, inTangent = 1.142857f, outTangent = 1.142857f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.7857143f, value = 0.9081632f, inTangent = 0.857143f, outTangent = 0.857143f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.8571429f, value = 0.9591837f, inTangent = 0.5714287f, outTangent = 0.5714287f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.9285714f, value = 0.9897959f, inTangent = 0.2857142f, outTangent = 0.2857142f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 1f, value = 1f, inTangent = 0.142857f, outTangent = 0.142857f, inWeight = 0.3333333f, outWeight = 0f, },
            }
    };

    public static readonly AnimationCurve QuadOutIn = new AnimationCurve
    {
        keys = new Keyframe[]
            {
            new Keyframe { time = 0f, value = 0f, inTangent = 1.857143f, outTangent = 1.857143f, inWeight = 0f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.07142857f, value = 0.1326531f, inTangent = 1.714286f, outTangent = 1.714286f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.1428571f, value = 0.244898f, inTangent = 1.428571f, outTangent = 1.428571f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.2142857f, value = 0.3367347f, inTangent = 1.142857f, outTangent = 1.142857f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.2857143f, value = 0.4081633f, inTangent = 0.857143f, outTangent = 0.857143f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.3571429f, value = 0.4591837f, inTangent = 0.5714285f, outTangent = 0.5714285f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.4285714f, value = 0.4897959f, inTangent = 0.2857142f, outTangent = 0.2857142f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.5f, value = 0.5f, inTangent = 0.1428571f, outTangent = 0.1428571f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.5714286f, value = 0.5102041f, inTangent = 0.2857142f, outTangent = 0.2857142f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.6428571f, value = 0.5408163f, inTangent = 0.5714287f, outTangent = 0.5714287f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.7142857f, value = 0.5918368f, inTangent = 0.857143f, outTangent = 0.857143f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.7857143f, value = 0.6632653f, inTangent = 1.142857f, outTangent = 1.142857f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.8571429f, value = 0.755102f, inTangent = 1.428571f, outTangent = 1.428571f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.9285714f, value = 0.8673469f, inTangent = 1.714286f, outTangent = 1.714286f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 1f, value = 1f, inTangent = 1.857143f, outTangent = 1.857143f, inWeight = 0.3333333f, outWeight = 0f, },
            }
    };

    public static readonly AnimationCurve ExpoOut = new AnimationCurve
    {
        keys = new Keyframe[]
            {
            new Keyframe { time = 0f, value = 0f, inTangent = 5.466904f, outTangent = 5.466904f, inWeight = 0f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.07142857f, value = 0.3904932f, inTangent = 4.399509f, outTangent = 4.399509f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.1428571f, value = 0.6285014f, inTangent = 2.681531f, outTangent = 2.681531f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.2142857f, value = 0.7735691f, inTangent = 1.634412f, outTangent = 1.634412f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.2857143f, value = 0.8619888f, inTangent = 0.9961847f, outTangent = 0.9961847f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.3571429f, value = 0.9158812f, inTangent = 0.6071814f, outTangent = 0.6071814f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.4285714f, value = 0.948729f, inTangent = 0.3700815f, outTangent = 0.3700815f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.5f, value = 0.96875f, inTangent = 0.2255671f, outTangent = 0.2255671f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.5714286f, value = 0.9809529f, inTangent = 0.1374848f, outTangent = 0.1374848f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.6428571f, value = 0.9883907f, inTangent = 0.08379783f, outTangent = 0.08379783f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.7142857f, value = 0.992924f, inTangent = 0.0510751f, outTangent = 0.0510751f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.7857143f, value = 0.9956871f, inTangent = 0.03113097f, outTangent = 0.03113097f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.8571429f, value = 0.9973713f, inTangent = 0.01897448f, outTangent = 0.01897448f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.9285714f, value = 0.9983978f, inTangent = 0.01840079f, outTangent = 0.01840079f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 1f, value = 1f, inTangent = 0.02243125f, outTangent = 0.02243125f, inWeight = 0.3333333f, outWeight = 0f, },
            }
    };

    public static readonly AnimationCurve ExpoIn = new AnimationCurve
    {
        keys = new Keyframe[]
            {
            new Keyframe { time = 0f, value = 0f, inTangent = 0.02243104f, outTangent = 0.02243104f, inWeight = 0f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.07142857f, value = 0.001602218f, inTangent = 0.01840098f, outTangent = 0.01840098f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.1428571f, value = 0.002628712f, inTangent = 0.01897442f, outTangent = 0.01897442f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.2142857f, value = 0.00431285f, inTangent = 0.03113078f, outTangent = 0.03113078f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.2857143f, value = 0.007075967f, inTangent = 0.05107537f, outTangent = 0.05107537f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.3571429f, value = 0.01160933f, inTangent = 0.08379786f, outTangent = 0.08379786f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.4285714f, value = 0.01904709f, inTangent = 0.1374847f, outTangent = 0.1374847f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.5f, value = 0.03125f, inTangent = 0.2255671f, outTangent = 0.2255671f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.5714286f, value = 0.05127097f, inTangent = 0.3700813f, outTangent = 0.3700813f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.6428571f, value = 0.08411875f, inTangent = 0.6071816f, outTangent = 0.6071816f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.7142857f, value = 0.1380112f, inTangent = 0.9961852f, outTangent = 0.9961852f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.7857143f, value = 0.2264309f, inTangent = 1.634412f, outTangent = 1.634412f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.8571429f, value = 0.3714986f, inTangent = 2.681531f, outTangent = 2.681531f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.9285714f, value = 0.6095067f, inTangent = 4.399509f, outTangent = 4.399509f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 1f, value = 1f, inTangent = 5.466904f, outTangent = 5.466904f, inWeight = 0.3333333f, outWeight = 0f, },
            }
    };

    public static readonly AnimationCurve ExpoInOut = new AnimationCurve
    {
        keys = new Keyframe[]
            {
            new Keyframe { time = 0f, value = 0f, inTangent = 0.01840098f, outTangent = 0.01840098f, inWeight = 0f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.07142857f, value = 0.001314356f, inTangent = 0.02476588f, outTangent = 0.02476588f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.1428571f, value = 0.003537983f, inTangent = 0.05746432f, outTangent = 0.05746432f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.2142857f, value = 0.009523544f, inTangent = 0.1546825f, outTangent = 0.1546825f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.2857143f, value = 0.02563548f, inTangent = 0.4163744f, outTangent = 0.4163744f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.3571429f, value = 0.0690056f, inTangent = 1.120797f, outTangent = 1.120797f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.4285714f, value = 0.1857493f, inTangent = 3.016961f, outTangent = 3.016961f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.5f, value = 0.5f, inTangent = 4.399509f, outTangent = 4.399509f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.5714286f, value = 0.8142508f, inTangent = 3.01696f, outTangent = 3.01696f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.6428571f, value = 0.9309944f, inTangent = 1.120797f, outTangent = 1.120797f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.7142857f, value = 0.9743645f, inTangent = 0.4163742f, outTangent = 0.4163742f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.7857143f, value = 0.9904764f, inTangent = 0.1546827f, outTangent = 0.1546827f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.8571429f, value = 0.996462f, inTangent = 0.05746459f, outTangent = 0.05746459f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.9285714f, value = 0.9986857f, inTangent = 0.02476567f, outTangent = 0.02476567f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 1f, value = 1f, inTangent = 0.01840078f, outTangent = 0.01840078f, inWeight = 0.3333333f, outWeight = 0f, },
            }
    };

    public static readonly AnimationCurve ExpoOutIn = new AnimationCurve
    {
        keys = new Keyframe[]
            {
            new Keyframe { time = 0f, value = 0f, inTangent = 4.39951f, outTangent = 4.39951f, inWeight = 0f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.07142857f, value = 0.3142507f, inTangent = 3.016961f, outTangent = 3.016961f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.1428571f, value = 0.4309944f, inTangent = 1.120797f, outTangent = 1.120797f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.2142857f, value = 0.4743645f, inTangent = 0.4163743f, outTangent = 0.4163743f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.2857143f, value = 0.4904765f, inTangent = 0.1546825f, outTangent = 0.1546825f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.3571429f, value = 0.496462f, inTangent = 0.05746439f, outTangent = 0.05746439f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.4285714f, value = 0.4986857f, inTangent = 0.02476588f, outTangent = 0.02476588f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.5f, value = 0.5f, inTangent = 0.01840078f, outTangent = 0.01840078f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.5714286f, value = 0.5013143f, inTangent = 0.02476567f, outTangent = 0.02476567f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.6428571f, value = 0.503538f, inTangent = 0.05746459f, outTangent = 0.05746459f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.7142857f, value = 0.5095236f, inTangent = 0.1546827f, outTangent = 0.1546827f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.7857143f, value = 0.5256355f, inTangent = 0.4163742f, outTangent = 0.4163742f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.8571429f, value = 0.5690056f, inTangent = 1.120797f, outTangent = 1.120797f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.9285714f, value = 0.6857492f, inTangent = 3.01696f, outTangent = 3.01696f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 1f, value = 1f, inTangent = 4.399509f, outTangent = 4.399509f, inWeight = 0.3333333f, outWeight = 0f, },
            }
    };

    public static readonly AnimationCurve CubicOut = new AnimationCurve
    {
        keys = new Keyframe[]
            {
            new Keyframe { time = 0f, value = 0f, inTangent = 2.790816f, outTangent = 2.790816f, inWeight = 0f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.07142857f, value = 0.199344f, inTangent = 2.591837f, outTangent = 2.591837f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.1428571f, value = 0.3702624f, inTangent = 2.209184f, outTangent = 2.209184f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.2142857f, value = 0.5149417f, inTangent = 1.857143f, outTangent = 1.857143f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.2857143f, value = 0.6355686f, inTangent = 1.535714f, outTangent = 1.535714f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.3571429f, value = 0.7343295f, inTangent = 1.244898f, outTangent = 1.244898f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.4285714f, value = 0.8134111f, inTangent = 0.9846939f, outTangent = 0.9846939f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.5f, value = 0.875f, inTangent = 0.7551023f, outTangent = 0.7551023f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.5714286f, value = 0.9212828f, inTangent = 0.5561225f, outTangent = 0.5561225f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.6428571f, value = 0.9544461f, inTangent = 0.3877551f, outTangent = 0.3877551f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.7142857f, value = 0.9766764f, inTangent = 0.2499999f, outTangent = 0.2499999f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.7857143f, value = 0.9901603f, inTangent = 0.1428571f, outTangent = 0.1428571f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.8571429f, value = 0.9970846f, inTangent = 0.06632661f, outTangent = 0.06632661f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.9285714f, value = 0.9996356f, inTangent = 0.0204081f, outTangent = 0.0204081f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 1f, value = 1f, inTangent = 0.005101917f, outTangent = 0.005101917f, inWeight = 0.3333333f, outWeight = 0f, },
            }
    };

    public static readonly AnimationCurve CubicIn = new AnimationCurve
    {
        keys = new Keyframe[]
            {
            new Keyframe { time = 0f, value = 0f, inTangent = 0.005102041f, outTangent = 0.005102041f, inWeight = 0f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.07142857f, value = 0.0003644315f, inTangent = 0.02040816f, outTangent = 0.02040816f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.1428571f, value = 0.002915452f, inTangent = 0.06632654f, outTangent = 0.06632654f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.2142857f, value = 0.00983965f, inTangent = 0.1428571f, outTangent = 0.1428571f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.2857143f, value = 0.02332362f, inTangent = 0.25f, outTangent = 0.25f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.3571429f, value = 0.04555394f, inTangent = 0.3877551f, outTangent = 0.3877551f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.4285714f, value = 0.0787172f, inTangent = 0.5561225f, outTangent = 0.5561225f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.5f, value = 0.125f, inTangent = 0.755102f, outTangent = 0.755102f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.5714286f, value = 0.1865889f, inTangent = 0.9846939f, outTangent = 0.9846939f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.6428571f, value = 0.2656705f, inTangent = 1.244898f, outTangent = 1.244898f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.7142857f, value = 0.3644315f, inTangent = 1.535714f, outTangent = 1.535714f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.7857143f, value = 0.4850583f, inTangent = 1.857143f, outTangent = 1.857143f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.8571429f, value = 0.6297376f, inTangent = 2.209184f, outTangent = 2.209184f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.9285714f, value = 0.8006559f, inTangent = 2.591837f, outTangent = 2.591837f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 1f, value = 1f, inTangent = 2.790816f, outTangent = 2.790816f, inWeight = 0.3333333f, outWeight = 0f, },
            }
    };

    public static readonly AnimationCurve CubicInOut = new AnimationCurve
    {
        keys = new Keyframe[]
            {
            new Keyframe { time = 0f, value = 0f, inTangent = 0.02040816f, outTangent = 0.02040816f, inWeight = 0f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.07142857f, value = 0.001457726f, inTangent = 0.08163266f, outTangent = 0.08163266f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.1428571f, value = 0.01166181f, inTangent = 0.2653061f, outTangent = 0.2653061f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.2142857f, value = 0.0393586f, inTangent = 0.5714286f, outTangent = 0.5714286f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.2857143f, value = 0.09329447f, inTangent = 1f, outTangent = 1f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.3571429f, value = 0.1822158f, inTangent = 1.55102f, outTangent = 1.55102f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.4285714f, value = 0.3148688f, inTangent = 2.22449f, outTangent = 2.22449f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.5f, value = 0.5f, inTangent = 2.591837f, outTangent = 2.591837f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.5714286f, value = 0.6851313f, inTangent = 2.22449f, outTangent = 2.22449f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.6428571f, value = 0.8177842f, inTangent = 1.55102f, outTangent = 1.55102f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.7142857f, value = 0.9067056f, inTangent = 0.9999999f, outTangent = 0.9999999f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.7857143f, value = 0.9606414f, inTangent = 0.5714284f, outTangent = 0.5714284f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.8571429f, value = 0.9883382f, inTangent = 0.265306f, outTangent = 0.265306f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.9285714f, value = 0.9985422f, inTangent = 0.08163282f, outTangent = 0.08163282f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 1f, value = 1f, inTangent = 0.0204085f, outTangent = 0.0204085f, inWeight = 0.3333333f, outWeight = 0f, },
            }
    };

    public static readonly AnimationCurve CubicOutIn = new AnimationCurve
    {
        keys = new Keyframe[]
            {
            new Keyframe { time = 0f, value = 0f, inTangent = 2.591837f, outTangent = 2.591837f, inWeight = 0f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.07142857f, value = 0.1851312f, inTangent = 2.22449f, outTangent = 2.22449f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.1428571f, value = 0.3177843f, inTangent = 1.55102f, outTangent = 1.55102f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.2142857f, value = 0.4067055f, inTangent = 0.9999999f, outTangent = 0.9999999f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.2857143f, value = 0.4606414f, inTangent = 0.5714287f, outTangent = 0.5714287f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.3571429f, value = 0.4883382f, inTangent = 0.2653061f, outTangent = 0.2653061f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.4285714f, value = 0.4985423f, inTangent = 0.08163259f, outTangent = 0.08163259f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.5f, value = 0.5f, inTangent = 0.0204083f, outTangent = 0.0204083f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.5714286f, value = 0.5014578f, inTangent = 0.08163282f, outTangent = 0.08163282f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.6428571f, value = 0.5116618f, inTangent = 0.265306f, outTangent = 0.265306f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.7142857f, value = 0.5393586f, inTangent = 0.5714284f, outTangent = 0.5714284f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.7857143f, value = 0.5932944f, inTangent = 0.9999999f, outTangent = 0.9999999f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.8571429f, value = 0.6822158f, inTangent = 1.55102f, outTangent = 1.55102f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.9285714f, value = 0.8148687f, inTangent = 2.22449f, outTangent = 2.22449f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 1f, value = 1f, inTangent = 2.591837f, outTangent = 2.591837f, inWeight = 0.3333333f, outWeight = 0f, },
            }
    };

    public static readonly AnimationCurve QuartOut = new AnimationCurve
    {
        keys = new Keyframe[]
            {
            new Keyframe { time = 0f, value = 0f, inTangent = 3.591472f, outTangent = 3.591472f, inWeight = 0f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.07142857f, value = 0.2565337f, inTangent = 3.221574f, outTangent = 3.221574f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.1428571f, value = 0.4602249f, inTangent = 2.536443f, outTangent = 2.536443f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.2142857f, value = 0.6188828f, inTangent = 1.956268f, outTangent = 1.956268f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.2857143f, value = 0.7396918f, inTangent = 1.472303f, outTangent = 1.472303f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.3571429f, value = 0.8292118f, inTangent = 1.075802f, outTangent = 1.075802f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.4285714f, value = 0.8933778f, inTangent = 0.7580177f, outTangent = 0.7580177f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.5f, value = 0.9375f, inTangent = 0.510204f, outTangent = 0.510204f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.5714286f, value = 0.9662641f, inTangent = 0.3236151f, outTangent = 0.3236151f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.6428571f, value = 0.9837307f, inTangent = 0.1895045f, outTangent = 0.1895045f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.7142857f, value = 0.9933361f, inTangent = 0.09912526f, outTangent = 0.09912526f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.7857143f, value = 0.9978915f, inTangent = 0.0437314f, outTangent = 0.0437314f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.8571429f, value = 0.9995835f, inTangent = 0.01457727f, outTangent = 0.01457727f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.9285714f, value = 0.999974f, inTangent = 0.002915622f, outTangent = 0.002915622f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 1f, value = 1f, inTangent = 0.0003646611f, outTangent = 0.0003646611f, inWeight = 0.3333333f, outWeight = 0f, },
            }
    };

    public static readonly AnimationCurve QuartIn = new AnimationCurve
    {
        keys = new Keyframe[]
            {
            new Keyframe { time = 0f, value = 0f, inTangent = 0.0003644315f, outTangent = 0.0003644315f, inWeight = 0f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.07142857f, value = 2.603082E-05f, inTangent = 0.002915452f, outTangent = 0.002915452f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.1428571f, value = 0.0004164932f, inTangent = 0.01457726f, outTangent = 0.01457726f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.2142857f, value = 0.002108497f, inTangent = 0.04373178f, outTangent = 0.04373178f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.2857143f, value = 0.006663891f, inTangent = 0.09912538f, outTangent = 0.09912538f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.3571429f, value = 0.01626926f, inTangent = 0.1895044f, outTangent = 0.1895044f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.4285714f, value = 0.03373595f, inTangent = 0.3236152f, outTangent = 0.3236152f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.5f, value = 0.0625f, inTangent = 0.5102041f, outTangent = 0.5102041f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.5714286f, value = 0.1066223f, inTangent = 0.7580175f, outTangent = 0.7580175f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.6428571f, value = 0.1707882f, inTangent = 1.075802f, outTangent = 1.075802f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.7142857f, value = 0.2603082f, inTangent = 1.472303f, outTangent = 1.472303f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.7857143f, value = 0.3811172f, inTangent = 1.956268f, outTangent = 1.956268f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.8571429f, value = 0.5397751f, inTangent = 2.536443f, outTangent = 2.536443f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.9285714f, value = 0.7434662f, inTangent = 3.221574f, outTangent = 3.221574f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 1f, value = 1f, inTangent = 3.591472f, outTangent = 3.591472f, inWeight = 0.3333333f, outWeight = 0f, },
            }
    };

    public static readonly AnimationCurve QuartInOut = new AnimationCurve
    {
        keys = new Keyframe[]
            {
            new Keyframe { time = 0f, value = 0f, inTangent = 0.002915452f, outTangent = 0.002915452f, inWeight = 0f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.07142857f, value = 0.0002082466f, inTangent = 0.02332362f, outTangent = 0.02332362f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.1428571f, value = 0.003331946f, inTangent = 0.1166181f, outTangent = 0.1166181f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.2142857f, value = 0.01686797f, inTangent = 0.3498542f, outTangent = 0.3498542f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.2857143f, value = 0.05331113f, inTangent = 0.793003f, outTangent = 0.793003f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.3571429f, value = 0.1301541f, inTangent = 1.516035f, outTangent = 1.516035f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.4285714f, value = 0.2698876f, inTangent = 2.588921f, outTangent = 2.588921f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.5f, value = 0.5f, inTangent = 3.221574f, outTangent = 3.221574f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.5714286f, value = 0.7301125f, inTangent = 2.588921f, outTangent = 2.588921f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.6428571f, value = 0.8698459f, inTangent = 1.516035f, outTangent = 1.516035f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.7142857f, value = 0.9466889f, inTangent = 0.7930029f, outTangent = 0.7930029f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.7857143f, value = 0.983132f, inTangent = 0.3498541f, outTangent = 0.3498541f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.8571429f, value = 0.996668f, inTangent = 0.1166181f, outTangent = 0.1166181f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.9285714f, value = 0.9997917f, inTangent = 0.02332373f, outTangent = 0.02332373f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 1f, value = 1f, inTangent = 0.00291562f, outTangent = 0.00291562f, inWeight = 0.3333333f, outWeight = 0f, },
            }
    };

    public static readonly AnimationCurve QuartOutIn = new AnimationCurve
    {
        keys = new Keyframe[]
            {
            new Keyframe { time = 0f, value = 0f, inTangent = 3.221574f, outTangent = 3.221574f, inWeight = 0f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.07142857f, value = 0.2301125f, inTangent = 2.588921f, outTangent = 2.588921f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.1428571f, value = 0.3698459f, inTangent = 1.516035f, outTangent = 1.516035f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.2142857f, value = 0.4466889f, inTangent = 0.793003f, outTangent = 0.793003f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.2857143f, value = 0.483132f, inTangent = 0.3498542f, outTangent = 0.3498542f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.3571429f, value = 0.4966681f, inTangent = 0.116618f, outTangent = 0.116618f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.4285714f, value = 0.4997917f, inTangent = 0.02332351f, outTangent = 0.02332351f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.5f, value = 0.5f, inTangent = 0.00291562f, outTangent = 0.00291562f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.5714286f, value = 0.5002083f, inTangent = 0.02332373f, outTangent = 0.02332373f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.6428571f, value = 0.503332f, inTangent = 0.1166181f, outTangent = 0.1166181f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.7142857f, value = 0.516868f, inTangent = 0.3498541f, outTangent = 0.3498541f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.7857143f, value = 0.5533111f, inTangent = 0.7930029f, outTangent = 0.7930029f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.8571429f, value = 0.6301541f, inTangent = 1.516035f, outTangent = 1.516035f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.9285714f, value = 0.7698875f, inTangent = 2.588921f, outTangent = 2.588921f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 1f, value = 1f, inTangent = 3.221574f, outTangent = 3.221574f, inWeight = 0.3333333f, outWeight = 0f, },
            }
    };

    public static readonly AnimationCurve QuintOut = new AnimationCurve
    {
        keys = new Keyframe[]
            {
            new Keyframe { time = 0f, value = 0f, inTangent = 4.334939f, outTangent = 4.334939f, inWeight = 0f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.07142857f, value = 0.3096385f, inTangent = 3.761349f, outTangent = 3.761349f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.1428571f, value = 0.5373356f, inTangent = 2.736386f, outTangent = 2.736386f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.2142857f, value = 0.7005507f, inTangent = 1.937109f, outTangent = 1.937109f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.2857143f, value = 0.8140656f, inTangent = 1.327598f, outTangent = 1.327598f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.3571429f, value = 0.8902076f, inTangent = 0.8750522f, outTangent = 0.8750522f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.4285714f, value = 0.939073f, inTangent = 0.5497969f, outTangent = 0.5497969f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.5f, value = 0.96875f, inTangent = 0.3252811f, outTangent = 0.3252811f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.5714286f, value = 0.9855418f, inTangent = 0.1780769f, outTangent = 0.1780769f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.6428571f, value = 0.9941896f, inTangent = 0.08788004f, outTangent = 0.08788004f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.7142857f, value = 0.998096f, inTangent = 0.03751045f, outTangent = 0.03751045f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.7857143f, value = 0.9995482f, inTangent = 0.01291126f, outTangent = 0.01291126f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.8571429f, value = 0.9999405f, inTangent = 0.003149688f, outTangent = 0.003149688f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.9285714f, value = 0.9999982f, inTangent = 0.0004163983f, outTangent = 0.0004163983f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 1f, value = 1f, inTangent = 2.586841E-05f, outTangent = 2.586841E-05f, inWeight = 0.3333333f, outWeight = 0f, },
            }
    };

    public static readonly AnimationCurve QuintIn = new AnimationCurve
    {
        keys = new Keyframe[]
            {
            new Keyframe { time = 0f, value = 0f, inTangent = 2.603082E-05f, outTangent = 2.603082E-05f, inWeight = 0f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.07142857f, value = 1.859345E-06f, inTangent = 0.0004164932f, outTangent = 0.0004164932f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.1428571f, value = 5.949903E-05f, inTangent = 0.00314973f, outTangent = 0.00314973f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.2142857f, value = 0.0004518207f, inTangent = 0.01291129f, outTangent = 0.01291129f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.2857143f, value = 0.001903969f, inTangent = 0.03751042f, outTangent = 0.03751042f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.3571429f, value = 0.005810452f, inTangent = 0.08788006f, outTangent = 0.08788006f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.4285714f, value = 0.01445826f, inTangent = 0.1780769f, outTangent = 0.1780769f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.5f, value = 0.03125f, inTangent = 0.3252811f, outTangent = 0.3252811f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.5714286f, value = 0.06092701f, inTangent = 0.549797f, outTangent = 0.549797f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.6428571f, value = 0.1097924f, inTangent = 0.8750521f, outTangent = 0.8750521f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.7142857f, value = 0.1859345f, inTangent = 1.327598f, outTangent = 1.327598f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.7857143f, value = 0.2994492f, inTangent = 1.93711f, outTangent = 1.93711f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.8571429f, value = 0.4626644f, inTangent = 2.736386f, outTangent = 2.736386f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.9285714f, value = 0.6903614f, inTangent = 3.761349f, outTangent = 3.761349f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 1f, value = 1f, inTangent = 4.334938f, outTangent = 4.334938f, inWeight = 0.3333333f, outWeight = 0f, },
            }
    };

    public static readonly AnimationCurve QuintInOut = new AnimationCurve
    {
        keys = new Keyframe[]
            {
            new Keyframe { time = 0f, value = 0f, inTangent = 0.0004164932f, outTangent = 0.0004164932f, inWeight = 0f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.07142857f, value = 2.974952E-05f, inTangent = 0.006663891f, outTangent = 0.006663891f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.1428571f, value = 0.0009519845f, inTangent = 0.05039568f, outTangent = 0.05039568f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.2142857f, value = 0.007229131f, inTangent = 0.2065806f, outTangent = 0.2065806f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.2857143f, value = 0.0304635f, inTangent = 0.6001667f, outTangent = 0.6001667f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.3571429f, value = 0.09296723f, inTangent = 1.406081f, outTangent = 1.406081f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.4285714f, value = 0.2313322f, inTangent = 2.84923f, outTangent = 2.84923f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.5f, value = 0.5f, inTangent = 3.761349f, outTangent = 3.761349f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.5714286f, value = 0.7686679f, inTangent = 2.849229f, outTangent = 2.849229f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.6428571f, value = 0.9070328f, inTangent = 1.406081f, outTangent = 1.406081f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.7142857f, value = 0.9695365f, inTangent = 0.6001664f, outTangent = 0.6001664f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.7857143f, value = 0.9927709f, inTangent = 0.2065806f, outTangent = 0.2065806f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.8571429f, value = 0.999048f, inTangent = 0.05039584f, outTangent = 0.05039584f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.9285714f, value = 0.9999703f, inTangent = 0.006664041f, outTangent = 0.006664041f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 1f, value = 1f, inTangent = 0.0004163979f, outTangent = 0.0004163979f, inWeight = 0.3333333f, outWeight = 0f, },
            }
    };

    public static readonly AnimationCurve QuintOutIn = new AnimationCurve
    {
        keys = new Keyframe[]
            {
            new Keyframe { time = 0f, value = 0f, inTangent = 3.761349f, outTangent = 3.761349f, inWeight = 0f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.07142857f, value = 0.2686678f, inTangent = 2.849229f, outTangent = 2.849229f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.1428571f, value = 0.4070328f, inTangent = 1.406081f, outTangent = 1.406081f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.2142857f, value = 0.4695365f, inTangent = 0.6001667f, outTangent = 0.6001667f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.2857143f, value = 0.4927709f, inTangent = 0.2065805f, outTangent = 0.2065805f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.3571429f, value = 0.499048f, inTangent = 0.05039564f, outTangent = 0.05039564f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.4285714f, value = 0.4999703f, inTangent = 0.00666383f, outTangent = 0.00666383f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.5f, value = 0.5f, inTangent = 0.000416398f, outTangent = 0.000416398f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.5714286f, value = 0.5000297f, inTangent = 0.006664041f, outTangent = 0.006664041f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.6428571f, value = 0.500952f, inTangent = 0.05039584f, outTangent = 0.05039584f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.7142857f, value = 0.5072291f, inTangent = 0.2065806f, outTangent = 0.2065806f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.7857143f, value = 0.5304635f, inTangent = 0.6001664f, outTangent = 0.6001664f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.8571429f, value = 0.5929672f, inTangent = 1.406081f, outTangent = 1.406081f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.9285714f, value = 0.7313321f, inTangent = 2.849229f, outTangent = 2.849229f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 1f, value = 1f, inTangent = 3.761349f, outTangent = 3.761349f, inWeight = 0.3333333f, outWeight = 0f, },
            }
    };

    public static readonly AnimationCurve CircOut = new AnimationCurve
    {
        keys = new Keyframe[]
            {
            new Keyframe { time = 0f, value = 0f, inTangent = 5.196152f, outTangent = 5.196152f, inWeight = 0f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.07142857f, value = 0.3711537f, inTangent = 3.605551f, outTangent = 3.605551f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.1428571f, value = 0.5150788f, inTangent = 1.732051f, outTangent = 1.732051f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.2142857f, value = 0.6185896f, inTangent = 1.293428f, outTangent = 1.293428f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.2857143f, value = 0.6998543f, inTangent = 1.031775f, outTangent = 1.031775f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.3571429f, value = 0.7659861f, inTangent = 0.8455831f, outTangent = 0.8455831f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.4285714f, value = 0.8206518f, inTangent = 0.7002752f, outTangent = 0.7002752f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.5f, value = 0.8660254f, inTangent = 0.5799924f, outTangent = 0.5799924f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.5714286f, value = 0.9035079f, inTangent = 0.4761708f, outTangent = 0.4761708f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.6428571f, value = 0.9340498f, inTangent = 0.3836487f, outTangent = 0.3836487f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.7142857f, value = 0.9583148f, inTangent = 0.2990485f, outTangent = 0.2990485f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.7857143f, value = 0.976771f, inTangent = 0.2199992f, outTangent = 0.2199992f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.8571429f, value = 0.9897433f, inTangent = 0.1447229f, outTangent = 0.1447229f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.9285714f, value = 0.9974457f, inTangent = 0.07179698f, outTangent = 0.07179698f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 1f, value = 1f, inTangent = 0.03576015f, outTangent = 0.03576015f, inWeight = 0.3333333f, outWeight = 0f, },
            }
    };

    public static readonly AnimationCurve CircIn = new AnimationCurve
    {
        keys = new Keyframe[]
            {
            new Keyframe { time = 0f, value = 0f, inTangent = 0.03575996f, outTangent = 0.03575996f, inWeight = 0f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.07142857f, value = 0.002554283f, inTangent = 0.07179677f, outTangent = 0.07179677f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.1428571f, value = 0.01025668f, inTangent = 0.1447229f, outTangent = 0.1447229f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.2142857f, value = 0.02322898f, inTangent = 0.2199993f, outTangent = 0.2199993f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.2857143f, value = 0.04168516f, inTangent = 0.2990488f, outTangent = 0.2990488f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.3571429f, value = 0.06595023f, inTangent = 0.3836486f, outTangent = 0.3836486f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.4285714f, value = 0.0964921f, inTangent = 0.4761706f, outTangent = 0.4761706f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.5f, value = 0.1339746f, inTangent = 0.5799928f, outTangent = 0.5799928f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.5714286f, value = 0.1793482f, inTangent = 0.7002752f, outTangent = 0.7002752f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.6428571f, value = 0.2340139f, inTangent = 0.8455831f, outTangent = 0.8455831f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.7142857f, value = 0.3001458f, inTangent = 1.031775f, outTangent = 1.031775f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.7857143f, value = 0.3814104f, inTangent = 1.293428f, outTangent = 1.293428f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.8571429f, value = 0.4849212f, inTangent = 1.732051f, outTangent = 1.732051f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.9285714f, value = 0.6288462f, inTangent = 3.605551f, outTangent = 3.605551f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 1f, value = 1f, inTangent = 5.196152f, outTangent = 5.196152f, inWeight = 0.3333333f, outWeight = 0f, },
            }
    };

    public static readonly AnimationCurve CircInOut = new AnimationCurve
    {
        keys = new Keyframe[]
            {
            new Keyframe { time = 0f, value = 0f, inTangent = 0.07179677f, outTangent = 0.07179677f, inWeight = 0f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.07142857f, value = 0.005128341f, inTangent = 0.145898f, outTangent = 0.145898f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.1428571f, value = 0.02084258f, inTangent = 0.301824f, outTangent = 0.301824f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.2142857f, value = 0.04824605f, inTangent = 0.4818207f, outTangent = 0.4818207f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.2857143f, value = 0.08967411f, inTangent = 0.712788f, outTangent = 0.712788f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.3571429f, value = 0.1500729f, inTangent = 1.069506f, outTangent = 1.069506f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.4285714f, value = 0.2424606f, inTangent = 2.44949f, outTangent = 2.44949f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.5f, value = 0.5f, inTangent = 3.605551f, outTangent = 3.605551f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.5714286f, value = 0.7575394f, inTangent = 2.449489f, outTangent = 2.449489f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.6428571f, value = 0.8499271f, inTangent = 1.069506f, outTangent = 1.069506f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.7142857f, value = 0.9103259f, inTangent = 0.7127879f, outTangent = 0.7127879f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.7857143f, value = 0.9517539f, inTangent = 0.4818206f, outTangent = 0.4818206f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.8571429f, value = 0.9791574f, inTangent = 0.3018243f, outTangent = 0.3018243f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.9285714f, value = 0.9948717f, inTangent = 0.1458979f, outTangent = 0.1458979f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 1f, value = 1f, inTangent = 0.07179651f, outTangent = 0.07179651f, inWeight = 0.3333333f, outWeight = 0f, },
            }
    };

    public static readonly AnimationCurve CircOutIn = new AnimationCurve
    {
        keys = new Keyframe[]
            {
            new Keyframe { time = 0f, value = 0f, inTangent = 3.605551f, outTangent = 3.605551f, inWeight = 0f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.07142857f, value = 0.2575394f, inTangent = 2.44949f, outTangent = 2.44949f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.1428571f, value = 0.3499271f, inTangent = 1.069506f, outTangent = 1.069506f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.2142857f, value = 0.4103259f, inTangent = 0.7127877f, outTangent = 0.7127877f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.2857143f, value = 0.4517539f, inTangent = 0.4818205f, outTangent = 0.4818205f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.3571429f, value = 0.4791574f, inTangent = 0.3018239f, outTangent = 0.3018239f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.4285714f, value = 0.4948716f, inTangent = 0.1458981f, outTangent = 0.1458981f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.5f, value = 0.5f, inTangent = 0.07179674f, outTangent = 0.07179674f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.5714286f, value = 0.5051283f, inTangent = 0.1458979f, outTangent = 0.1458979f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.6428571f, value = 0.5208426f, inTangent = 0.3018243f, outTangent = 0.3018243f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.7142857f, value = 0.5482461f, inTangent = 0.4818206f, outTangent = 0.4818206f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.7857143f, value = 0.5896741f, inTangent = 0.7127879f, outTangent = 0.7127879f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.8571429f, value = 0.6500729f, inTangent = 1.069506f, outTangent = 1.069506f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.9285714f, value = 0.7424606f, inTangent = 2.449489f, outTangent = 2.449489f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 1f, value = 1f, inTangent = 3.60555f, outTangent = 3.60555f, inWeight = 0.3333333f, outWeight = 0f, },
            }
    };

    public static readonly AnimationCurve SineOut = new AnimationCurve
    {
        keys = new Keyframe[]
            {
            new Keyframe { time = 0f, value = 0f, inTangent = 1.567503f, outTangent = 1.567503f, inWeight = 0f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.07142857f, value = 0.1119645f, inTangent = 1.557647f, outTangent = 1.557647f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.1428571f, value = 0.2225209f, inTangent = 1.528202f, outTangent = 1.528202f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.2142857f, value = 0.3302791f, inTangent = 1.47954f, outTangent = 1.47954f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.2857143f, value = 0.4338838f, inTangent = 1.412271f, outTangent = 1.412271f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.3571429f, value = 0.5320321f, inTangent = 1.327242f, outTangent = 1.327242f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.4285714f, value = 0.6234898f, inTangent = 1.225523f, outTangent = 1.225523f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.5f, value = 0.7071068f, inTangent = 1.108392f, outTangent = 1.108392f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.5714286f, value = 0.7818315f, inTangent = 0.9773221f, outTangent = 0.9773221f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.6428571f, value = 0.8467242f, inTangent = 0.8339619f, outTangent = 0.8339619f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.7142857f, value = 0.9009689f, inTangent = 0.6801136f, outTangent = 0.6801136f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.7857143f, value = 0.9438833f, inTangent = 0.517713f, outTangent = 0.517713f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.8571429f, value = 0.9749279f, inTangent = 0.3488022f, outTangent = 0.3488022f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.9285714f, value = 0.9937122f, inTangent = 0.1755047f, outTangent = 0.1755047f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 1f, value = 1f, inTangent = 0.08802935f, outTangent = 0.08802935f, inWeight = 0.3333333f, outWeight = 0f, },
            }
    };

    public static readonly AnimationCurve SineIn = new AnimationCurve
    {
        keys = new Keyframe[]
            {
            new Keyframe { time = 0f, value = 0f, inTangent = 0.08802906f, outTangent = 0.08802906f, inWeight = 0f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.07142857f, value = 0.006287791f, inTangent = 0.1755046f, outTangent = 0.1755046f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.1428571f, value = 0.02507209f, inTangent = 0.3488021f, outTangent = 0.3488021f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.2142857f, value = 0.05611667f, inTangent = 0.5177133f, outTangent = 0.5177133f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.2857143f, value = 0.09903114f, inTangent = 0.6801139f, outTangent = 0.6801139f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.3571429f, value = 0.1532758f, inTangent = 0.8339617f, outTangent = 0.8339617f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.4285714f, value = 0.2181685f, inTangent = 0.977322f, outTangent = 0.977322f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.5f, value = 0.2928932f, inTangent = 1.108392f, outTangent = 1.108392f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.5714286f, value = 0.3765102f, inTangent = 1.225523f, outTangent = 1.225523f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.6428571f, value = 0.4679679f, inTangent = 1.327242f, outTangent = 1.327242f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.7142857f, value = 0.5661163f, inTangent = 1.412271f, outTangent = 1.412271f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.7857143f, value = 0.6697209f, inTangent = 1.47954f, outTangent = 1.47954f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.8571429f, value = 0.7774791f, inTangent = 1.528202f, outTangent = 1.528202f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.9285714f, value = 0.8880355f, inTangent = 1.557647f, outTangent = 1.557647f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 1f, value = 1f, inTangent = 1.567503f, outTangent = 1.567503f, inWeight = 0.3333333f, outWeight = 0f, },
            }
    };

    public static readonly AnimationCurve SineInOut = new AnimationCurve
    {
        keys = new Keyframe[]
            {
            new Keyframe { time = 0f, value = 0f, inTangent = 1.557647f, outTangent = 1.557647f, inWeight = 0f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.07142857f, value = 0.1112605f, inTangent = 1.518593f, outTangent = 1.518593f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.1428571f, value = 0.2169419f, inTangent = 1.403391f, outTangent = 1.403391f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.2142857f, value = 0.3117449f, inTangent = 1.217817f, outTangent = 1.217817f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.2857143f, value = 0.3909158f, inTangent = 0.9711769f, outTangent = 0.9711769f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.3571429f, value = 0.4504845f, inTangent = 0.6758375f, outTangent = 0.6758375f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.4285714f, value = 0.487464f, inTangent = 0.3466088f, outTangent = 0.3466088f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.5f, value = 0.5f, inTangent = 0.1755047f, outTangent = 0.1755047f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.5714286f, value = 0.512536f, inTangent = 0.3466089f, outTangent = 0.3466089f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.6428571f, value = 0.5495155f, inTangent = 0.6758374f, outTangent = 0.6758374f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.7142857f, value = 0.6090842f, inTangent = 0.9711767f, outTangent = 0.9711767f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.7857143f, value = 0.6882551f, inTangent = 1.217817f, outTangent = 1.217817f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.8571429f, value = 0.7830582f, inTangent = 1.403391f, outTangent = 1.403391f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.9285714f, value = 0.8887395f, inTangent = 1.518593f, outTangent = 1.518593f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 1f, value = 1f, inTangent = 1.557647f, outTangent = 1.557647f, inWeight = 0.3333333f, outWeight = 0f, },
            }
    };

    public static readonly AnimationCurve SineOutIn = new AnimationCurve
    {
        keys = new Keyframe[]
            {
            new Keyframe { time = 0f, value = 0f, inTangent = 1.557647f, outTangent = 1.557647f, inWeight = 0f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.07142857f, value = 0.1112605f, inTangent = 1.518593f, outTangent = 1.518593f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.1428571f, value = 0.2169419f, inTangent = 1.403391f, outTangent = 1.403391f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.2142857f, value = 0.3117449f, inTangent = 1.217817f, outTangent = 1.217817f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.2857143f, value = 0.3909158f, inTangent = 0.9711769f, outTangent = 0.9711769f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.3571429f, value = 0.4504845f, inTangent = 0.6758375f, outTangent = 0.6758375f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.4285714f, value = 0.487464f, inTangent = 0.3466088f, outTangent = 0.3466088f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.5f, value = 0.5f, inTangent = 0.1755047f, outTangent = 0.1755047f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.5714286f, value = 0.512536f, inTangent = 0.3466089f, outTangent = 0.3466089f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.6428571f, value = 0.5495155f, inTangent = 0.6758374f, outTangent = 0.6758374f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.7142857f, value = 0.6090842f, inTangent = 0.9711767f, outTangent = 0.9711767f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.7857143f, value = 0.6882551f, inTangent = 1.217817f, outTangent = 1.217817f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.8571429f, value = 0.7830582f, inTangent = 1.403391f, outTangent = 1.403391f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.9285714f, value = 0.8887395f, inTangent = 1.518593f, outTangent = 1.518593f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 1f, value = 1f, inTangent = 1.557647f, outTangent = 1.557647f, inWeight = 0.3333333f, outWeight = 0f, },
            }
    };

    public static readonly AnimationCurve ElasticOut = new AnimationCurve
    {
        keys = new Keyframe[]
            {
            new Keyframe { time = 0f, value = 0f, inTangent = 11.86602f, outTangent = 11.86602f, inWeight = 0f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.03448276f, value = 0.409173f, inTangent = 13.36681f, outTangent = 13.36681f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.06896552f, value = 0.9218489f, inTangent = 12.5395f, outTangent = 12.5395f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.1034483f, value = 1.273966f, inTangent = 6.529908f, outTangent = 6.529908f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.137931f, value = 1.372187f, inTangent = -0.05842757f, outTangent = -0.05842757f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.1724138f, value = 1.269937f, inTangent = -4.117598f, outTangent = -4.117598f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.2068966f, value = 1.088215f, inTangent = -4.82935f, outTangent = -4.82935f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.2413793f, value = 0.9368784f, inTangent = -3.153702f, outTangent = -3.153702f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.2758621f, value = 0.8707185f, inTangent = -0.7323782f, outTangent = -0.7323782f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.3103448f, value = 0.8863695f, inTangent = 1.089884f, outTangent = 1.089884f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.3448276f, value = 0.9458829f, inTangent = 1.741942f, outTangent = 1.741942f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.3793103f, value = 1.006503f, inTangent = 1.382641f, outTangent = 1.382641f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.4137931f, value = 1.041237f, inTangent = 0.5537963f, outTangent = 0.5537963f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.4482759f, value = 1.044696f, inTangent = -0.2028436f, outTangent = -0.2028436f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.4827586f, value = 1.027248f, inTangent = -0.583047f, outTangent = -0.583047f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.5172414f, value = 1.004486f, inTangent = -0.5631964f, outTangent = -0.5631964f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.5517241f, value = 0.9884071f, inTangent = -0.3040128f, outTangent = -0.3040128f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.5862069f, value = 0.9835198f, inTangent = -0.01005478f, outTangent = -0.01005478f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.6206896f, value = 0.9877137f, inTangent = 0.176607f, outTangent = 0.176607f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.6551724f, value = 0.9956996f, inTangent = 0.2149224f, outTangent = 0.2149224f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.6896552f, value = 1.002536f, inTangent = 0.1444683f, outTangent = 0.1444683f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.7241379f, value = 1.005663f, inTangent = 0.03745741f, outTangent = 0.03745741f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.7586207f, value = 1.005119f, inTangent = -0.04530833f, outTangent = -0.04530833f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.7931035f, value = 1.002538f, inTangent = -0.07676159f, outTangent = -0.07676159f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.8275862f, value = 0.9998253f, inTangent = -0.06261531f, outTangent = -0.06261531f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.862069f, value = 0.9982199f, inTangent = -0.02639648f, outTangent = -0.02639648f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.8965517f, value = 0.9980049f, inTangent = 0.007629748f, outTangent = 0.007629748f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.9310345f, value = 0.9987461f, inTangent = 0.02538181f, outTangent = 0.02538181f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.9655172f, value = 0.9997553f, inTangent = 0.01818161f, outTangent = 0.01818161f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 1f, value = 1f, inTangent = 0.007095631f, outTangent = 0.007095631f, inWeight = 0.3333333f, outWeight = 0f, },
            }
    };

    public static readonly AnimationCurve ElasticIn = new AnimationCurve
    {
        keys = new Keyframe[]
            {
            new Keyframe { time = 0f, value = -0.0004882813f, inTangent = 0.02125652f, outTangent = 0.02125652f, inWeight = 0f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.03448276f, value = 0.0002447022f, inTangent = 0.02526196f, outTangent = 0.02526196f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.06896552f, value = 0.001253923f, inTangent = 0.02538158f, outTangent = 0.02538158f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.1034483f, value = 0.001995156f, inTangent = 0.007629462f, outTangent = 0.007629462f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.137931f, value = 0.001780093f, inTangent = -0.026397f, outTangent = -0.026397f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.1724138f, value = 0.0001746732f, inTangent = -0.06261525f, outTangent = -0.06261525f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.2068966f, value = -0.0025382f, inTangent = -0.07676195f, outTangent = -0.07676195f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.2413793f, value = -0.005119253f, inTangent = -0.04530762f, outTangent = -0.04530762f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.2758621f, value = -0.005662863f, inTangent = 0.03745796f, outTangent = 0.03745796f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.3103448f, value = -0.002535947f, inTangent = 0.1444672f, outTangent = 0.1444672f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.3448276f, value = 0.004300395f, inTangent = 0.2149224f, outTangent = 0.2149224f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.3793103f, value = 0.01228629f, inTangent = 0.1766069f, outTangent = 0.1766069f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.4137931f, value = 0.01648018f, inTangent = -0.01005499f, outTangent = -0.01005499f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.4482759f, value = 0.01159284f, inTangent = -0.3040125f, outTangent = -0.3040125f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.4827586f, value = -0.004486193f, inTangent = -0.5631961f, outTangent = -0.5631961f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.5172414f, value = -0.02724825f, inTangent = -0.5830472f, outTangent = -0.5830472f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.5517241f, value = -0.04469634f, inTangent = -0.2028442f, outTangent = -0.2028442f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.5862069f, value = -0.04123751f, inTangent = 0.5537972f, outTangent = 0.5537972f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.6206896f, value = -0.006503469f, inTangent = 1.382642f, outTangent = 1.382642f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.6551724f, value = 0.05411709f, inTangent = 1.741942f, outTangent = 1.741942f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.6896552f, value = 0.1136305f, inTangent = 1.089884f, outTangent = 1.089884f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.7241379f, value = 0.1292815f, inTangent = -0.732377f, outTangent = -0.732377f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.7586207f, value = 0.06312168f, inTangent = -3.153701f, outTangent = -3.153701f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.7931035f, value = -0.08821519f, inTangent = -4.829349f, outTangent = -4.829349f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.8275862f, value = -0.269937f, inTangent = -4.117596f, outTangent = -4.117596f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.862069f, value = -0.3721873f, inTangent = -0.05842793f, outTangent = -0.05842793f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.8965517f, value = -0.2739664f, inTangent = 6.529908f, outTangent = 6.529908f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.9310345f, value = 0.07815138f, inTangent = 12.53951f, outTangent = 12.53951f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.9655172f, value = 0.5908267f, inTangent = 13.36681f, outTangent = 13.36681f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 1f, value = 1f, inTangent = 11.86602f, outTangent = 11.86602f, inWeight = 0.3333333f, outWeight = 0f, },
            }
    };

    public static readonly AnimationCurve ElasticInOut = new AnimationCurve
    {
        keys = new Keyframe[]
            {
            new Keyframe { time = 0f, value = 8.478915E-05f, inTangent = 0.01826913f, outTangent = 0.01826913f, inWeight = 0f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.03448276f, value = 0.0007147591f, inTangent = 0.01466564f, outTangent = 0.01466564f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.06896552f, value = 0.001096213f, inTangent = -0.008042136f, outTangent = -0.008042136f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.1034483f, value = 0.0001601289f, inTangent = -0.05296766f, outTangent = -0.05296766f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.137931f, value = -0.002556729f, inTangent = -0.07666016f, outTangent = -0.07666016f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.1724138f, value = -0.005126779f, inTangent = -0.00343734f, outTangent = -0.00343734f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.2068966f, value = -0.002793787f, inTangent = 0.1930943f, outTangent = 0.1930943f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.2413793f, value = 0.008190063f, inTangent = 0.3646758f, outTangent = 0.3646758f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.2758621f, value = 0.02235627f, inTangent = 0.1695123f, outTangent = 0.1695123f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.3103448f, value = 0.01988057f, inTangent = -0.636393f, outTangent = -0.636393f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.3448276f, value = -0.02153293f, inTangent = -1.613391f, outTangent = -1.613391f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.3793103f, value = -0.09138775f, inTangent = -1.31678f, outTangent = -1.31678f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.4137931f, value = -0.1123453f, inTangent = 1.771262f, outTangent = 1.771262f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.4482759f, value = 0.03076817f, inTangent = 6.688684f, outTangent = 6.688684f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.4827586f, value = 0.3489431f, inTangent = 8.994185f, outTangent = 8.994185f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.5172414f, value = 0.6510566f, inTangent = 8.994186f, outTangent = 8.994186f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.5517241f, value = 0.9692318f, inTangent = 6.688683f, outTangent = 6.688683f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.5862069f, value = 1.112345f, inTangent = 1.771259f, outTangent = 1.771259f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.6206896f, value = 1.091388f, inTangent = -1.31678f, outTangent = -1.31678f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.6551724f, value = 1.021533f, inTangent = -1.61339f, outTangent = -1.61339f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.6896552f, value = 0.9801194f, inTangent = -0.6363926f, outTangent = -0.6363926f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.7241379f, value = 0.9776437f, inTangent = 0.1695121f, outTangent = 0.1695121f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.7586207f, value = 0.9918099f, inTangent = 0.3646757f, outTangent = 0.3646757f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.7931035f, value = 1.002794f, inTangent = 0.1930954f, outTangent = 0.1930954f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.8275862f, value = 1.005127f, inTangent = -0.003438119f, outTangent = -0.003438119f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.862069f, value = 1.002557f, inTangent = -0.0766614f, outTangent = -0.0766614f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.8965517f, value = 0.9998398f, inTangent = -0.05296659f, outTangent = -0.05296659f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.9310345f, value = 0.9989038f, inTangent = -0.008041994f, outTangent = -0.008041994f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.9655172f, value = 0.9992852f, inTangent = 0.01589474f, outTangent = 0.01589474f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 1f, value = 1f, inTangent = 0.02072858f, outTangent = 0.02072858f, inWeight = 0.3333333f, outWeight = 0f, },
            }
    };

    public static readonly AnimationCurve ElasticOutIn = new AnimationCurve
    {
        keys = new Keyframe[]
            {
            new Keyframe { time = 0f, value = 0f, inTangent = 13.36681f, outTangent = 13.36681f, inWeight = 0f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.03448276f, value = 0.4609244f, inTangent = 9.948359f, outTangent = 9.948359f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.06896552f, value = 0.6860937f, inTangent = 1.206155f, outTangent = 1.206155f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.1034483f, value = 0.5441076f, inTangent = -3.63565f, outTangent = -3.63565f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.137931f, value = 0.4353592f, inTangent = -1.031909f, outTangent = -1.031909f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.1724138f, value = 0.4729415f, inTangent = 1.236263f, outTangent = 1.236263f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.2068966f, value = 0.5206187f, inTangent = 0.5898987f, outTangent = 0.5898987f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.2413793f, value = 0.5136241f, inTangent = -0.38302f, outTangent = -0.38302f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.2758621f, value = 0.4942036f, inTangent = -0.2866256f, outTangent = -0.2866256f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.3103448f, value = 0.4938568f, inTangent = 0.1024338f, outTangent = 0.1024338f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.3448276f, value = 0.501268f, inTangent = 0.1261899f, outTangent = 0.1261899f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.3793103f, value = 0.5025596f, inTangent = -0.01965212f, outTangent = -0.01965212f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.4137931f, value = 0.4999126f, inTangent = -0.05157902f, outTangent = -0.05157902f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.4482759f, value = 0.4990024f, inTangent = -0.0005073259f, outTangent = -0.0005073259f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.4827586f, value = 0.4998777f, inTangent = 0.01623916f, outTangent = 0.01623916f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.5172414f, value = 0.5001224f, inTangent = 0.01623915f, outTangent = 0.01623915f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.5517241f, value = 0.5009976f, inTangent = -0.0005081883f, outTangent = -0.0005081883f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.5862069f, value = 0.5000873f, inTangent = -0.05157994f, outTangent = -0.05157994f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.6206896f, value = 0.4974404f, inTangent = -0.01965177f, outTangent = -0.01965177f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.6551724f, value = 0.498732f, inTangent = 0.1261903f, outTangent = 0.1261903f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.6896552f, value = 0.5061432f, inTangent = 0.1024338f, outTangent = 0.1024338f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.7241379f, value = 0.5057964f, inTangent = -0.2866255f, outTangent = -0.2866255f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.7586207f, value = 0.4863759f, inTangent = -0.3830202f, outTangent = -0.3830202f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.7931035f, value = 0.4793812f, inTangent = 0.5898994f, outTangent = 0.5898994f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.8275862f, value = 0.5270586f, inTangent = 1.236264f, outTangent = 1.236264f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.862069f, value = 0.5646408f, inTangent = -1.031908f, outTangent = -1.031908f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.8965517f, value = 0.4558924f, inTangent = -3.635648f, outTangent = -3.635648f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.9310345f, value = 0.3139063f, inTangent = 1.206157f, outTangent = 1.206157f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.9655172f, value = 0.5390753f, inTangent = 9.948359f, outTangent = 9.948359f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 1f, value = 1f, inTangent = 13.36681f, outTangent = 13.36681f, inWeight = 0.3333333f, outWeight = 0f, },
            }
    };

    public static readonly AnimationCurve BounceOut = new AnimationCurve
    {
        keys = new Keyframe[]
            {
            new Keyframe { time = 0f, value = 0f, inTangent = 0.2607759f, outTangent = 0.2607759f, inWeight = 0f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.03448276f, value = 0.008992271f, inTangent = 0.5215517f, outTangent = 0.5215517f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.06896552f, value = 0.03596909f, inTangent = 1.043103f, outTangent = 1.043103f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.1034483f, value = 0.08093044f, inTangent = 1.564655f, outTangent = 1.564655f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.137931f, value = 0.1438763f, inTangent = 2.086207f, outTangent = 2.086207f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.1724138f, value = 0.2248068f, inTangent = 2.607758f, outTangent = 2.607758f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.2068966f, value = 0.3237218f, inTangent = 3.12931f, outTangent = 3.12931f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.2413793f, value = 0.4406213f, inTangent = 3.650863f, outTangent = 3.650863f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.2758621f, value = 0.5755054f, inTangent = 4.172414f, outTangent = 4.172414f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.3103448f, value = 0.7283739f, inTangent = 4.693965f, outTangent = 4.693965f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.3448276f, value = 0.8992271f, inTangent = 3.340518f, outTangent = 3.340518f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.3793103f, value = 0.9587545f, inTangent = -0.2629309f, outTangent = -0.2629309f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.4137931f, value = 0.8810939f, inTangent = -1.99138f, outTangent = -1.99138f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.4482759f, value = 0.8214179f, inTangent = -1.469828f, outTangent = -1.469828f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.4827586f, value = 0.7797265f, inTangent = -0.9482753f, outTangent = -0.9482753f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.5172414f, value = 0.7560197f, inTangent = -0.4267243f, outTangent = -0.4267243f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.5517241f, value = 0.7502972f, inTangent = 0.09482737f, outTangent = 0.09482737f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.5862069f, value = 0.7625595f, inTangent = 0.6163795f, outTangent = 0.6163795f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.6206896f, value = 0.7928061f, inTangent = 1.137931f, outTangent = 1.137931f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.6551724f, value = 0.8410375f, inTangent = 1.659483f, outTangent = 1.659483f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.6896552f, value = 0.9072533f, inTangent = 2.181034f, outTangent = 2.181034f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.7241379f, value = 0.9914535f, inTangent = 0.8275869f, outTangent = 0.8275869f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.7586207f, value = 0.9643282f, inTangent = -0.7133607f, outTangent = -0.7133607f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.7931035f, value = 0.9422562f, inTangent = -0.3793103f, outTangent = -0.3793103f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.8275862f, value = 0.9381688f, inTangent = 0.1422423f, outTangent = 0.1422423f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.862069f, value = 0.952066f, inTangent = 0.6637936f, outTangent = 0.6637936f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.8965517f, value = 0.9839477f, inTangent = 0.5290947f, outTangent = 0.5290947f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.9310345f, value = 0.9885553f, inTangent = 0.01939579f, outTangent = 0.01939579f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.9655172f, value = 0.9852853f, inTangent = 0.1659478f, outTangent = 0.1659478f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 1f, value = 1f, inTangent = 0.4267249f, outTangent = 0.4267249f, inWeight = 0.3333333f, outWeight = 0f, },
            }
    };

    public static readonly AnimationCurve BounceIn = new AnimationCurve
    {
        keys = new Keyframe[]
            {
            new Keyframe { time = 0f, value = 0f, inTangent = 0.4267241f, outTangent = 0.4267241f, inWeight = 0f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.03448276f, value = 0.01471463f, inTangent = 0.1659483f, outTangent = 0.1659483f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.06896552f, value = 0.01144471f, inTangent = 0.0193966f, outTangent = 0.0193966f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.1034483f, value = 0.01605232f, inTangent = 0.5290948f, outTangent = 0.5290948f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.137931f, value = 0.04793401f, inTangent = 0.6637931f, outTangent = 0.6637931f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.1724138f, value = 0.06183115f, inTangent = 0.1422413f, outTangent = 0.1422413f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.2068966f, value = 0.05774375f, inTangent = -0.3793104f, outTangent = -0.3793104f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.2413793f, value = 0.03567182f, inTangent = -0.7133621f, outTangent = -0.7133621f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.2758621f, value = 0.008546371f, inTangent = 0.8275863f, outTangent = 0.8275863f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.3103448f, value = 0.0927467f, inTangent = 2.181035f, outTangent = 2.181035f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.3448276f, value = 0.1589625f, inTangent = 1.659483f, outTangent = 1.659483f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.3793103f, value = 0.2071938f, inTangent = 1.137931f, outTangent = 1.137931f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.4137931f, value = 0.2374406f, inTangent = 0.6163793f, outTangent = 0.6163793f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.4482759f, value = 0.2497027f, inTangent = 0.09482744f, outTangent = 0.09482744f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.4827586f, value = 0.2439804f, inTangent = -0.4267239f, outTangent = -0.4267239f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.5172414f, value = 0.2202735f, inTangent = -0.9482756f, outTangent = -0.9482756f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.5517241f, value = 0.178582f, inTangent = -1.469828f, outTangent = -1.469828f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.5862069f, value = 0.118906f, inTangent = -1.991379f, outTangent = -1.991379f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.6206896f, value = 0.0412456f, inTangent = -0.2629328f, outTangent = -0.2629328f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.6551724f, value = 0.1007729f, inTangent = 3.340515f, outTangent = 3.340515f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.6896552f, value = 0.2716261f, inTangent = 4.693966f, outTangent = 4.693966f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.7241379f, value = 0.4244945f, inTangent = 4.172414f, outTangent = 4.172414f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.7586207f, value = 0.5593787f, inTangent = 3.650863f, outTangent = 3.650863f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.7931035f, value = 0.6762783f, inTangent = 3.12931f, outTangent = 3.12931f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.8275862f, value = 0.7751933f, inTangent = 2.607758f, outTangent = 2.607758f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.862069f, value = 0.8561236f, inTangent = 2.086207f, outTangent = 2.086207f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.8965517f, value = 0.9190696f, inTangent = 1.564655f, outTangent = 1.564655f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.9310345f, value = 0.9640309f, inTangent = 1.043103f, outTangent = 1.043103f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.9655172f, value = 0.9910077f, inTangent = 0.521552f, outTangent = 0.521552f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 1f, value = 1f, inTangent = 0.2607752f, outTangent = 0.2607752f, inWeight = 0.3333333f, outWeight = 0f, },
            }
    };

    public static readonly AnimationCurve BounceInOut = new AnimationCurve
    {
        keys = new Keyframe[]
            {
            new Keyframe { time = 0f, value = 0f, inTangent = 0.1659483f, outTangent = 0.1659483f, inWeight = 0f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.03448276f, value = 0.005722354f, inTangent = 0.3475216f, outTangent = 0.3475216f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.06896552f, value = 0.023967f, inTangent = 0.3356681f, outTangent = 0.3356681f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.1034483f, value = 0.02887188f, inTangent = -0.2855604f, outTangent = -0.2855604f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.137931f, value = 0.004273186f, inTangent = 0.7338361f, outTangent = 0.7338361f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.1724138f, value = 0.07948127f, inTangent = 1.659483f, outTangent = 1.659483f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.2068966f, value = 0.1187203f, inTangent = 0.6163792f, outTangent = 0.6163792f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.2413793f, value = 0.1219902f, inTangent = -0.4267241f, outTangent = -0.4267241f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.2758621f, value = 0.08929102f, inTangent = -1.469827f, outTangent = -1.469827f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.3103448f, value = 0.0206228f, inTangent = 0.6745681f, outTangent = 0.6745681f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.3448276f, value = 0.135813f, inTangent = 3.756464f, outTangent = 3.756464f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.3793103f, value = 0.2796893f, inTangent = 3.650862f, outTangent = 3.650862f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.4137931f, value = 0.3875966f, inTangent = 2.607759f, outTangent = 2.607759f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.4482759f, value = 0.4595348f, inTangent = 1.564655f, outTangent = 1.564655f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.4827586f, value = 0.4955039f, inTangent = 0.6519391f, outTangent = 0.6519391f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.5172414f, value = 0.5044961f, inTangent = 0.6519395f, outTangent = 0.6519395f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.5517241f, value = 0.5404652f, inTangent = 1.564656f, outTangent = 1.564656f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.5862069f, value = 0.6124035f, inTangent = 2.607759f, outTangent = 2.607759f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.6206896f, value = 0.7203106f, inTangent = 3.650861f, outTangent = 3.650861f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.6551724f, value = 0.8641869f, inTangent = 3.756464f, outTangent = 3.756464f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.6896552f, value = 0.9793772f, inTangent = 0.6745681f, outTangent = 0.6745681f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.7241379f, value = 0.910709f, inTangent = -1.469828f, outTangent = -1.469828f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.7586207f, value = 0.8780098f, inTangent = -0.4267249f, outTangent = -0.4267249f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.7931035f, value = 0.8812797f, inTangent = 0.6163796f, outTangent = 0.6163796f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.8275862f, value = 0.9205188f, inTangent = 1.659483f, outTangent = 1.659483f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.862069f, value = 0.9957268f, inTangent = 0.733837f, outTangent = 0.733837f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.8965517f, value = 0.9711281f, inTangent = -0.285559f, outTangent = -0.285559f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.9310345f, value = 0.976033f, inTangent = 0.3356688f, outTangent = 0.3356688f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.9655172f, value = 0.9942777f, inTangent = 0.3475213f, outTangent = 0.3475213f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 1f, value = 1f, inTangent = 0.1659479f, outTangent = 0.1659479f, inWeight = 0.3333333f, outWeight = 0f, },
            }
    };

    public static readonly AnimationCurve BounceOutIn = new AnimationCurve
    {
        keys = new Keyframe[]
            {
            new Keyframe { time = 0f, value = 0f, inTangent = 0.5215517f, outTangent = 0.5215517f, inWeight = 0f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.03448276f, value = 0.01798454f, inTangent = 1.043103f, outTangent = 1.043103f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.06896552f, value = 0.07193817f, inTangent = 2.086207f, outTangent = 2.086207f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.1034483f, value = 0.1618609f, inTangent = 3.129311f, outTangent = 3.129311f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.137931f, value = 0.2877527f, inTangent = 4.172414f, outTangent = 4.172414f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.1724138f, value = 0.4496136f, inTangent = 2.215517f, outTangent = 2.215517f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.2068966f, value = 0.440547f, inTangent = -0.8663799f, outTangent = -0.8663799f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.2413793f, value = 0.3898633f, inTangent = -0.9482761f, outTangent = -0.9482761f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.2758621f, value = 0.3751486f, inTangent = 0.09482753f, outTangent = 0.09482753f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.3103448f, value = 0.3964031f, inTangent = 1.137931f, outTangent = 1.137931f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.3448276f, value = 0.4536267f, inTangent = 1.243534f, outTangent = 1.243534f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.3793103f, value = 0.4821641f, inTangent = 0.2241376f, outTangent = 0.2241376f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.4137931f, value = 0.4690844f, inTangent = 0.1422418f, outTangent = 0.1422418f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.4482759f, value = 0.4919738f, inTangent = 0.3415949f, outTangent = 0.3415949f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.4827586f, value = 0.4926427f, inTangent = 0.2230601f, outTangent = 0.2230601f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.5172414f, value = 0.5073573f, inTangent = 0.223061f, outTangent = 0.223061f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.5517241f, value = 0.5080262f, inTangent = 0.3415946f, outTangent = 0.3415946f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.5862069f, value = 0.5309156f, inTangent = 0.1422407f, outTangent = 0.1422407f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.6206896f, value = 0.5178359f, inTangent = 0.2241378f, outTangent = 0.2241378f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.6551724f, value = 0.5463734f, inTangent = 1.243534f, outTangent = 1.243534f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.6896552f, value = 0.6035969f, inTangent = 1.137931f, outTangent = 1.137931f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.7241379f, value = 0.6248513f, inTangent = 0.09482789f, outTangent = 0.09482789f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.7586207f, value = 0.6101367f, inTangent = -0.9482753f, outTangent = -0.9482753f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.7931035f, value = 0.559453f, inTangent = -0.8663765f, outTangent = -0.8663765f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.8275862f, value = 0.5503866f, inTangent = 2.215519f, outTangent = 2.215519f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.862069f, value = 0.7122473f, inTangent = 4.172413f, outTangent = 4.172413f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.8965517f, value = 0.8381391f, inTangent = 3.129311f, outTangent = 3.129311f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.9310345f, value = 0.9280619f, inTangent = 2.086207f, outTangent = 2.086207f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.9655172f, value = 0.9820154f, inTangent = 1.043103f, outTangent = 1.043103f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 1f, value = 1f, inTangent = 0.5215522f, outTangent = 0.5215522f, inWeight = 0.3333333f, outWeight = 0f, },
            }
    };

    public static readonly AnimationCurve BackOut = new AnimationCurve
    {
        keys = new Keyframe[]
            {
            new Keyframe { time = 0f, value = 2.220446E-16f, inTangent = 4.483994f, outTangent = 4.483994f, inWeight = 0f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.03448276f, value = 0.1546205f, inTangent = 4.272832f, outTangent = 4.272832f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.06896552f, value = 0.2946781f, inTangent = 3.860146f, outTangent = 3.860146f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.1034483f, value = 0.4208375f, inTangent = 3.466734f, outTangent = 3.466734f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.137931f, value = 0.5337632f, inTangent = 3.092596f, outTangent = 3.092596f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.1724138f, value = 0.6341199f, inTangent = 2.737731f, outTangent = 2.737731f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.2068966f, value = 0.7225723f, inTangent = 2.402142f, outTangent = 2.402142f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.2413793f, value = 0.7997848f, inTangent = 2.085826f, outTangent = 2.085826f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.2758621f, value = 0.8664224f, inTangent = 1.788785f, outTangent = 1.788785f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.3103448f, value = 0.9231493f, inTangent = 1.511016f, outTangent = 1.511016f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.3448276f, value = 0.9706303f, inTangent = 1.252523f, outTangent = 1.252523f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.3793103f, value = 1.00953f, inTangent = 1.013302f, outTangent = 1.013302f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.4137931f, value = 1.040513f, inTangent = 0.7933576f, outTangent = 0.7933576f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.4482759f, value = 1.064245f, inTangent = 0.5926871f, outTangent = 0.5926871f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.4827586f, value = 1.081388f, inTangent = 0.4112877f, outTangent = 0.4112877f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.5172414f, value = 1.092609f, inTangent = 0.2491648f, outTangent = 0.2491648f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.5517241f, value = 1.098572f, inTangent = 0.1063152f, outTangent = 0.1063152f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.5862069f, value = 1.099941f, inTangent = -0.01725947f, outTangent = -0.01725947f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.6206896f, value = 1.097382f, inTangent = -0.1215609f, outTangent = -0.1215609f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.6551724f, value = 1.091558f, inTangent = -0.2065892f, outTangent = -0.2065892f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.6896552f, value = 1.083134f, inTangent = -0.2723411f, outTangent = -0.2723411f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.7241379f, value = 1.072776f, inTangent = -0.3188214f, outTangent = -0.3188214f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.7586207f, value = 1.061146f, inTangent = -0.3460265f, outTangent = -0.3460265f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.7931035f, value = 1.048912f, inTangent = -0.3539553f, outTangent = -0.3539553f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.8275862f, value = 1.036736f, inTangent = -0.342613f, outTangent = -0.342613f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.862069f, value = 1.025283f, inTangent = -0.3119972f, outTangent = -0.3119972f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.8965517f, value = 1.015219f, inTangent = -0.2621062f, outTangent = -0.2621062f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.9310345f, value = 1.007207f, inTangent = -0.1929408f, outTangent = -0.1929408f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.9655172f, value = 1.001912f, inTangent = -0.1045004f, outTangent = -0.1045004f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 1f, value = 1f, inTangent = -0.05546173f, outTangent = -0.05546173f, inWeight = 0.3333333f, outWeight = 0f, },
            }
    };

    public static readonly AnimationCurve BackIn = new AnimationCurve
    {
        keys = new Keyframe[]
            {
            new Keyframe { time = 0f, value = 0f, inTangent = -0.05546283f, outTangent = -0.05546283f, inWeight = 0f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.03448276f, value = -0.001912511f, inTangent = -0.104501f, outTangent = -0.104501f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.06896552f, value = -0.007206964f, inTangent = -0.1929402f, outTangent = -0.1929402f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.1034483f, value = -0.01521874f, inTangent = -0.2621054f, outTangent = -0.2621054f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.137931f, value = -0.0252832f, inTangent = -0.3119966f, outTangent = -0.3119966f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.1724138f, value = -0.03673574f, inTangent = -0.3426137f, outTangent = -0.3426137f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.2068966f, value = -0.04891174f, inTangent = -0.3539567f, outTangent = -0.3539567f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.2413793f, value = -0.06114655f, inTangent = -0.3460257f, outTangent = -0.3460257f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.2758621f, value = -0.07277557f, inTangent = -0.3188207f, outTangent = -0.3188207f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.3103448f, value = -0.08313418f, inTangent = -0.2723417f, outTangent = -0.2723417f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.3448276f, value = -0.09155776f, inTangent = -0.2065884f, outTangent = -0.2065884f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.3793103f, value = -0.09738166f, inTangent = -0.1215611f, outTangent = -0.1215611f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.4137931f, value = -0.09994128f, inTangent = -0.01725983f, outTangent = -0.01725983f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.4482759f, value = -0.09857199f, inTangent = 0.1063154f, outTangent = 0.1063154f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.4827586f, value = -0.09260918f, inTangent = 0.2491649f, outTangent = 0.2491649f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.5172414f, value = -0.08138821f, inTangent = 0.4112883f, outTangent = 0.4112883f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.5517241f, value = -0.06424446f, inTangent = 0.5926858f, outTangent = 0.5926858f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.5862069f, value = -0.04051331f, inTangent = 0.7933574f, outTangent = 0.7933574f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.6206896f, value = -0.009530187f, inTangent = 1.013303f, outTangent = 1.013303f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.6551724f, value = 0.02936963f, inTangent = 1.252523f, outTangent = 1.252523f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.6896552f, value = 0.07685073f, inTangent = 1.511016f, outTangent = 1.511016f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.7241379f, value = 0.1335776f, inTangent = 1.788784f, outTangent = 1.788784f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.7586207f, value = 0.2002151f, inTangent = 2.085826f, outTangent = 2.085826f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.7931035f, value = 0.2774278f, inTangent = 2.402142f, outTangent = 2.402142f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.8275862f, value = 0.3658802f, inTangent = 2.737731f, outTangent = 2.737731f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.862069f, value = 0.4662368f, inTangent = 3.092596f, outTangent = 3.092596f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.8965517f, value = 0.5791626f, inTangent = 3.466734f, outTangent = 3.466734f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.9310345f, value = 0.705322f, inTangent = 3.860146f, outTangent = 3.860146f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.9655172f, value = 0.8453795f, inTangent = 4.272832f, outTangent = 4.272832f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 1f, value = 1f, inTangent = 4.483993f, outTangent = 4.483993f, inWeight = 0.3333333f, outWeight = 0f, },
            }
    };

    public static readonly AnimationCurve BackInOut = new AnimationCurve
    {
        keys = new Keyframe[]
            {
            new Keyframe { time = 0f, value = 0f, inTangent = -0.161861f, outTangent = -0.161861f, inWeight = 0f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.03448276f, value = -0.005581414f, inTangent = -0.2895255f, outTangent = -0.2895255f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.06896552f, value = -0.01996728f, inTangent = -0.4935597f, outTangent = -0.4935597f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.1034483f, value = -0.03962002f, inTangent = -0.5950043f, outTangent = -0.5950043f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.137931f, value = -0.06100206f, inTangent = -0.5938594f, outTangent = -0.5938594f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.1724138f, value = -0.08057584f, inTangent = -0.4901248f, outTangent = -0.4901248f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.2068966f, value = -0.09480377f, inTangent = -0.2838008f, outTangent = -0.2838008f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.2413793f, value = -0.1001483f, inTangent = 0.02511276f, outTangent = 0.02511276f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.2758621f, value = -0.09307186f, inTangent = 0.4366161f, outTangent = 0.4366161f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.3103448f, value = -0.07003686f, inTangent = 0.9507088f, outTangent = 0.9507088f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.3448276f, value = -0.02750571f, inTangent = 1.567391f, outTangent = 1.567391f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.3793103f, value = 0.03805909f, inTangent = 2.286663f, outTangent = 2.286663f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.4137931f, value = 0.1301952f, inTangent = 3.108525f, outTangent = 3.108525f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.4482759f, value = 0.2524401f, inTangent = 4.032976f, outTangent = 4.032976f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.4827586f, value = 0.4083314f, inTangent = 4.918813f, outTangent = 4.918813f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.5172414f, value = 0.5916685f, inTangent = 4.918813f, outTangent = 4.918813f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.5517241f, value = 0.7475599f, inTangent = 4.032975f, outTangent = 4.032975f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.5862069f, value = 0.8698049f, inTangent = 3.108524f, outTangent = 3.108524f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.6206896f, value = 0.9619408f, inTangent = 2.286664f, outTangent = 2.286664f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.6551724f, value = 1.027506f, inTangent = 1.567392f, outTangent = 1.567392f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.6896552f, value = 1.070037f, inTangent = 0.9507079f, outTangent = 0.9507079f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.7241379f, value = 1.093072f, inTangent = 0.4366161f, outTangent = 0.4366161f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.7586207f, value = 1.100148f, inTangent = 0.02511387f, outTangent = 0.02511387f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.7931035f, value = 1.094804f, inTangent = -0.283801f, outTangent = -0.283801f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.8275862f, value = 1.080576f, inTangent = -0.4901263f, outTangent = -0.4901263f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.862069f, value = 1.061002f, inTangent = -0.593859f, outTangent = -0.593859f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.8965517f, value = 1.03962f, inTangent = -0.5950028f, outTangent = -0.5950028f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.9310345f, value = 1.019967f, inTangent = -0.4935607f, outTangent = -0.4935607f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.9655172f, value = 1.005581f, inTangent = -0.2895263f, outTangent = -0.2895263f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 1f, value = 1f, inTangent = -0.1618599f, outTangent = -0.1618599f, inWeight = 0.3333333f, outWeight = 0f, },
            }
    };

    public static readonly AnimationCurve BackOutIn = new AnimationCurve
    {
        keys = new Keyframe[]
            {
            new Keyframe { time = 0f, value = 1.110223E-16f, inTangent = 4.272832f, outTangent = 4.272832f, inWeight = 0f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.03448276f, value = 0.147339f, inTangent = 3.869783f, outTangent = 3.869783f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.06896552f, value = 0.2668816f, inTangent = 3.102233f, outTangent = 3.102233f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.1034483f, value = 0.3612861f, inTangent = 2.411779f, outTangent = 2.411779f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.137931f, value = 0.4332112f, inTangent = 1.798421f, outTangent = 1.798421f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.1724138f, value = 0.4853152f, inTangent = 1.262159f, outTangent = 1.262159f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.2068966f, value = 0.5202566f, inTangent = 0.8029947f, outTangent = 0.8029947f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.2413793f, value = 0.5406941f, inTangent = 0.4209259f, outTangent = 0.4209259f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.2758621f, value = 0.549286f, inTangent = 0.1159527f, outTangent = 0.1159527f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.3103448f, value = 0.5486909f, inTangent = -0.1119243f, outTangent = -0.1119243f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.3448276f, value = 0.5415671f, inTangent = -0.2627053f, outTangent = -0.2627053f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.3793103f, value = 0.5305732f, inTangent = -0.3363884f, outTangent = -0.3363884f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.4137931f, value = 0.5183679f, inTangent = -0.3329762f, outTangent = -0.3329762f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.4482759f, value = 0.5076094f, inTangent = -0.252469f, outTangent = -0.252469f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.4827586f, value = 0.5009562f, inTangent = -0.1242017f, outTangent = -0.1242017f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.5172414f, value = 0.4990437f, inTangent = -0.1242012f, outTangent = -0.1242012f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.5517241f, value = 0.4923906f, inTangent = -0.2524683f, outTangent = -0.2524683f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.5862069f, value = 0.4816321f, inTangent = -0.3329768f, outTangent = -0.3329768f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.6206896f, value = 0.4694267f, inTangent = -0.3363885f, outTangent = -0.3363885f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.6551724f, value = 0.4584329f, inTangent = -0.2627043f, outTangent = -0.2627043f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.6896552f, value = 0.4513092f, inTangent = -0.1119243f, outTangent = -0.1119243f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.7241379f, value = 0.450714f, inTangent = 0.1159522f, outTangent = 0.1159522f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.7586207f, value = 0.4593059f, inTangent = 0.4209252f, outTangent = 0.4209252f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.7931035f, value = 0.4797433f, inTangent = 0.8029947f, outTangent = 0.8029947f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.8275862f, value = 0.5146849f, inTangent = 1.26216f, outTangent = 1.26216f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.862069f, value = 0.5667888f, inTangent = 1.798421f, outTangent = 1.798421f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.8965517f, value = 0.6387139f, inTangent = 2.411779f, outTangent = 2.411779f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.9310345f, value = 0.7331185f, inTangent = 3.102233f, outTangent = 3.102233f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 0.9655172f, value = 0.8526609f, inTangent = 3.869783f, outTangent = 3.869783f, inWeight = 0.3333333f, outWeight = 0.3333333f, },
            new Keyframe { time = 1f, value = 1f, inTangent = 4.272832f, outTangent = 4.272832f, inWeight = 0.3333333f, outWeight = 0f, },
            }
    };
}
