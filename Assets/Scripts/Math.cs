using UnityEngine;

public class S_Math{
    public static float det(Vector2[] mat){
        return mat[0].x * mat[1].y - mat[1].x * mat[0].y;
    }

    public static Vector2[] inverse(Vector2[] mat){
        float d = det(mat);
        Vector2[] result = {new Vector2(mat[1].y, -mat[0].y), new Vector2(-mat[1].x, mat[0].x)};
        result[0] = result[0] / d;
        result[1] = result[1] / d;
        return result;
    }

    public static Vector2 mult(Vector2[] mat, Vector2 vec){
        float i1 = mat[0].x * vec.x + mat[1].x * vec.y;
        float i2 = mat[0].y * vec.x + mat[1].y * vec.y;
        return new Vector2(i1, i2);
    }

    public static float calculateShare(Vector2 toSplit, Vector2 part){
        part.Normalize();
        Vector2[]  vectorBasis = {new Vector2(part.x, part.y), 
                                    new Vector2(part.y, -part.x)};

        if(S_Math.det(vectorBasis) == 0){
            Debug.LogWarning("Could not inverse direction matrix because of determinant == 0");
            return 0;
        }

        vectorBasis = S_Math.inverse(vectorBasis);

        Vector2 transformedSplit = S_Math.mult(vectorBasis, toSplit);
        return transformedSplit[0]/transformedSplit.magnitude;
    }
}

