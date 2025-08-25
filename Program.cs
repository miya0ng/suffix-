var codes = new string[]
{
    "ABC_001_10Test",
    "ABC_1_10test",
    "ABC_1",
    "xyz_00005_Test",
    "XYZ_5_test",
    "XYZ_5_TEst",
    "XYZ_5",
    "ABC_1_10"
};

CustomStringSort test = new CustomStringSort(codes);

test.Print();
public class CustomStringSort
{
    //1.    PREFIX 기준 정렬:
    //PREFIX를 비교할 때 대소문자를 구분하지 않고 사전 순으로 정렬한다.
    //예: "abc"와 "AbC"와 "ABC"는 모두 동일하게 취급한다.

    //2.    NUMERIC PARTS 기준 정렬:
    //PREFIX가 동일한 경우, NUMERIC PART들을 왼쪽부터 차례로 비교한다.
    //각 숫자 그룹을 정수로 변환하여 비교하며, 선행 0은 무시한다.
    //예를 들어 N1 = 001과 N1 = 1은 같은 값으로 취급한다.
    //첫 번째 숫자 그룹에서 크기가 갈리면 그에 따라 정렬을 완료한다. 같다면 두 번째 숫자 그룹 비교로 넘어간다.이 과정을 모든 숫자 그룹에 대해 반복한다.
    //만약 두 이름에서 숫자 그룹의 개수가 다르고, 앞부분 숫자 그룹들 모두 같은 값을 가진다면, 숫자 그룹이 적은 쪽이 앞선다.

    //3.    SUFFIX 기준 정렬:
    //PREFIX와 NUMERIC PARTS가 모두 동일하다면 SUFFIX를 비교한다.
    //SUFFIX 비교 시에도 대소문자 구분 없이 사전순으로 비교한다.
    //SUFFIX가 없는 경우가 있다면 없는 SUFFIX가 있는 SUFFIX보다 앞선다.
    //예: ABC_1 vs ABC_1Test → 접미사가 없는 ABC_1이 앞선다.ABC_1Test vs ABC_1test → 
    //    대소문자 구분 없이 비교하므로 같음.이 경우 둘은 사실상 같은 우선순위를 가지지만, 정렬 안정성(입력 순서 유지)에 따라 원래 입력 순서대로 유지한다.

    //4.    입력 순서 유지(Stable Sort):
    //위 모든 기준으로도 순위를 매길 수 없다면(즉, 완전히 동일한 PREFIX, NUMERIC PARTS, SUFFIX를 가진 경우), 입력 시 주어진 순서를 유지한다.

    public string[] source;

    public string[] splitSource;
    public enum Element { PREFIX, NUMERIC, SUFFIX }

    public (string source, Element element) taggedSource;

    public CustomStringSort(string[] source)
    {
        this.source = source;
        splitSource = new string[source.Length];
    }

    public void Sort()
    {
        for(int k = 0; k< source.Length; k++)
        {
            splitSource = source[k].Split('_');
 
            for (int i = 1; i < splitSource.Length; i++)
            {
                taggedSource = (splitSource[i], Element.NUMERIC);
            }
            foreach (char item in splitSource[splitSource.Length - 1])
            {
                if (!char.IsDigit(item))
                {
                    break;
                }
                taggedSource = (splitSource[splitSource.Length - 1], Element.SUFFIX);
            }
        }
    }

    public void Print()
    {
        foreach(string item in splitSource)
        {
            Console.WriteLine($"{item}");
        }
    }
}