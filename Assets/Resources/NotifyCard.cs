using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "New CardInfo", menuName = "Card/NewNotifyCard")]
public class NotifyCard : Card
{
    [Header("All Choice")]
    public string Paragraph;
    [FormerlySerializedAs("Description")] public string DescriptionChoice;
    public int Money;
    public int Happiness;
    public int Power;
    public int Stability;
    public List<PossibleChainCard> possibleChainCards;

    public void setValue(int money,int happiness,int power,int stability)
    {
        Money = money;
        Happiness = happiness;
        Power = power;
        Stability = stability;
    }

    public void setDefault()
    {
        int randomNumber = Random.Range(0, 8);
        if(randomNumber == 0)
        {
            cardName = "ขยันทำงาน";
            description =
                "การนั่งเก้าอี้บ้าๆทำงานที่ไม่รู้จบทั้งวัน ทั้งแก้ปัญหาเล็กน้อยจุกจิก โดนผู้คนประนามด่า อยู่รอบข้าง แต่คุณก็ไม่ย้อท้อ คุณยังคงพยายามทำงานต่อไป และวันนี้ความพยายามของคุณมันก็ส่งผลแล้ว";
            paragraph = "ด้วยความสามารถด้านการบริหารของคุณ บัดนี้มันได้สำริดผลแล้วละ";

            Paragraph = "แจ่ม...";
            DescriptionChoice = "โชคดีชะมัด";
            
        }
        else if (randomNumber == 1)
        {
            cardName = "ผลงานที่สำเร็จ";
            description =
                "ดูเหมือนว่าจะมีโปรเจคที่รัฐบาลชุดก่อนได้สร้างทิ้งไว้ แล้วบังเอิญว่ามันกลับมาเสร็จในยุคสมัยของคุณพอดี";
            paragraph = "โปรเจคระยะยาว บังเอิญเสร็จในยุคของคุณพอดี";

            Paragraph = "เคลมเครดิตตึงๆ";
            DescriptionChoice = "ช่วยไม่ได้อ่ะนะ";
        }
        else if (randomNumber == 2)
        {
            cardName = "สุนทรพจน์ที่ดี";
            description =
                "สุนทรพจน์ที่เตรียมการมาอย่างดี สุนทรพจน์เกี่ยวกับการเมือง การปกครอง เพื่อนร่วมชาติ และอำนาจ แต่สุนทรพจน์เป็นเพียงแค่วาจาไว้เริ่มต้น บัดนี้ได้เวลาลงมือทำแล้ว";
            paragraph = "สุนทรพจน์มีไว้ทางเสียง ตอนนี้ถึงเวลาลงมือแล้ว";

            Paragraph = "ดำเนินการตามแผนการ";
            DescriptionChoice = "ดำเนินการ";
        }
        else if (randomNumber == 3)
        {
            cardName = "สภาเห็นชอบ";
            description =
                "ด้วยทักษะการปกครองและการเจรจาของคุณทุกคนต่าง เห็นชอบและมีความสุขที่คุณได้เข้ามาปกครอง";
            paragraph = "การเจรจาเพื่อทุกคน";

            Paragraph = "จับมือไปด้วยกัน";
            DescriptionChoice = "หวานหมู";
        }
        else if (randomNumber == 4)
        {
            cardName = "ใส่ใจ";
            description =
                "ความใส่ใจในทุกอย่างของคุณ ทำให้ลูกน้องของคุณมองคุณเป็นตัวอย่าง และปฏิบัติตาม และเพิ่มประสิทธิภาพการทำงานขึ้นมากกว่า 200 %";
            paragraph = "ด้วยความเคารพ";

            Paragraph = "น่าเอ็นดู";
            DescriptionChoice = "นับถือ นับถือ";
        }
        else if (randomNumber == 5)
        {
            cardName = "บุญหล่นทับ";
            description =
                "ด้วยบุญบารมีที่คุณสะสมไว้ตั้งแต่ชาติบางก่อน และด้วยคนในที่ทำงานคุณคนนึงสามารถเล่นหมอผี ถอนบุญจากชาติบางก่อนออกมาได้ คุณจึงถอดบารมีออกมาบางส่วน";
            paragraph = "บุญบารมีของคุณที่สะสมไว้ตั้งแต่ชาติบางก่อนถูกนำมาใช้เพื่อบ้านเมือง";

            Paragraph = "เลื่อมใสจริมๆ";
            DescriptionChoice = "สาธุ";
        }
        else if (randomNumber == 6)
        {
            cardName = "เสามงคล";
            description =
                "ได้มีเจ้าแม่ชิงแส่ เอาเสามงคลตั้งไว้ใจกลางประเทศคุณ เพื่อดักเก็บพลังอันไร้ขีดจำกัด";
            paragraph = "ได้มีเจ้าแม่ชิงแส่ เอาเสามงคลตั้งไว้ใจกลางประเทศคุณ";

            Paragraph = "ความเจริญ";
            DescriptionChoice = "เดินติดแล้ว เดินติดแล้ว ผีติดบัค";
        }
        else if (randomNumber == 7)
        {
            cardName = "วิสัยทัศน์กว้างไกล";
            description =
                "แนวคิดใหม่ เทคโนโลยีใหม่ ยุคสมัยใหม่ คุณคือผู้บุกเบิกและสร้าง วิสัยทัศน์ให้ประเทศไปข้างหน้า";
            paragraph = "การมองการไกลของคุณ ทำให้การทำงานในการบริหารประเทศนั้นง่ายขึ้น";

            Paragraph = "อนาคตจงเจริญ";
            DescriptionChoice = "เพื่ออนาคตอันสดใส";
        }
        
        
    }
}
