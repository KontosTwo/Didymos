// GENERATED CODE
using NUnit.Framework;
using static Unity.Mathematics.math;
namespace Unity.Mathematics.Tests
{
    [TestFixture]
    public class TestUint2
    {
        [Test]
        public void uint2_constructor()
        {
            uint2 a = new uint2(1, 2);
            Assert.AreEqual(a.x, 1);
            Assert.AreEqual(a.y, 2);
        }

        [Test]
        public void uint2_scalar_constructor()
        {
            uint2 a = new uint2(17u);
            Assert.AreEqual(a.x, 17u);
            Assert.AreEqual(a.y, 17u);
        }

        [Test]
        public void uint2_static_constructor()
        {
            uint2 a = uint2(1, 2);
            Assert.AreEqual(a.x, 1);
            Assert.AreEqual(a.y, 2);
        }

        [Test]
        public void uint2_static_scalar_constructor()
        {
            uint2 a = uint2(17u);
            Assert.AreEqual(a.x, 17u);
            Assert.AreEqual(a.y, 17u);
        }

        [Test]
        public void uint2_operator_equal_wide_wide()
        {
            uint2 a0 = uint2(2105871082, 35218899);
            uint2 b0 = uint2(1477587886, 579629692);
            bool2 r0 = bool2(false, false);
            TestUtils.AreEqual(a0 == b0, r0);

            uint2 a1 = uint2(1550755093, 764676020);
            uint2 b1 = uint2(540974792, 208405066);
            bool2 r1 = bool2(false, false);
            TestUtils.AreEqual(a1 == b1, r1);

            uint2 a2 = uint2(606743782, 1208156098);
            uint2 b2 = uint2(2063397938, 1060167409);
            bool2 r2 = bool2(false, false);
            TestUtils.AreEqual(a2 == b2, r2);

            uint2 a3 = uint2(1023640014, 1038468316);
            uint2 b3 = uint2(362592601, 2097545362);
            bool2 r3 = bool2(false, false);
            TestUtils.AreEqual(a3 == b3, r3);
        }

        [Test]
        public void uint2_operator_equal_wide_scalar()
        {
            uint2 a0 = uint2(437822262, 2020661134);
            uint b0 = (1332833578);
            bool2 r0 = bool2(false, false);
            TestUtils.AreEqual(a0 == b0, r0);

            uint2 a1 = uint2(541786900, 23716499);
            uint b1 = (853113810);
            bool2 r1 = bool2(false, false);
            TestUtils.AreEqual(a1 == b1, r1);

            uint2 a2 = uint2(179951405, 948838849);
            uint b2 = (1409026299);
            bool2 r2 = bool2(false, false);
            TestUtils.AreEqual(a2 == b2, r2);

            uint2 a3 = uint2(953202998, 1926262965);
            uint b3 = (691955848);
            bool2 r3 = bool2(false, false);
            TestUtils.AreEqual(a3 == b3, r3);
        }

        [Test]
        public void uint2_operator_equal_scalar_wide()
        {
            uint a0 = (542329200);
            uint2 b0 = uint2(1115584594, 684107773);
            bool2 r0 = bool2(false, false);
            TestUtils.AreEqual(a0 == b0, r0);

            uint a1 = (1928988941);
            uint2 b1 = uint2(890709324, 641152437);
            bool2 r1 = bool2(false, false);
            TestUtils.AreEqual(a1 == b1, r1);

            uint a2 = (1410341302);
            uint2 b2 = uint2(497505660, 1068223109);
            bool2 r2 = bool2(false, false);
            TestUtils.AreEqual(a2 == b2, r2);

            uint a3 = (2014009435);
            uint2 b3 = uint2(213835595, 1592428361);
            bool2 r3 = bool2(false, false);
            TestUtils.AreEqual(a3 == b3, r3);
        }

        [Test]
        public void uint2_operator_not_equal_wide_wide()
        {
            uint2 a0 = uint2(1977284100, 1293292704);
            uint2 b0 = uint2(1514195556, 957972049);
            bool2 r0 = bool2(true, true);
            TestUtils.AreEqual(a0 != b0, r0);

            uint2 a1 = uint2(1547283851, 422428953);
            uint2 b1 = uint2(507667364, 581861672);
            bool2 r1 = bool2(true, true);
            TestUtils.AreEqual(a1 != b1, r1);

            uint2 a2 = uint2(195833190, 1711544892);
            uint2 b2 = uint2(1128094576, 940096636);
            bool2 r2 = bool2(true, true);
            TestUtils.AreEqual(a2 != b2, r2);

            uint2 a3 = uint2(5606053, 737069074);
            uint2 b3 = uint2(57559040, 181752616);
            bool2 r3 = bool2(true, true);
            TestUtils.AreEqual(a3 != b3, r3);
        }

        [Test]
        public void uint2_operator_not_equal_wide_scalar()
        {
            uint2 a0 = uint2(1038269360, 1427812625);
            uint b0 = (768873026);
            bool2 r0 = bool2(true, true);
            TestUtils.AreEqual(a0 != b0, r0);

            uint2 a1 = uint2(103361237, 1523584313);
            uint b1 = (1347017023);
            bool2 r1 = bool2(true, true);
            TestUtils.AreEqual(a1 != b1, r1);

            uint2 a2 = uint2(150442802, 719113717);
            uint b2 = (660334527);
            bool2 r2 = bool2(true, true);
            TestUtils.AreEqual(a2 != b2, r2);

            uint2 a3 = uint2(262959423, 506946952);
            uint b3 = (484398043);
            bool2 r3 = bool2(true, true);
            TestUtils.AreEqual(a3 != b3, r3);
        }

        [Test]
        public void uint2_operator_not_equal_scalar_wide()
        {
            uint a0 = (1652127596);
            uint2 b0 = uint2(953791238, 271722683);
            bool2 r0 = bool2(true, true);
            TestUtils.AreEqual(a0 != b0, r0);

            uint a1 = (1278885987);
            uint2 b1 = uint2(735128017, 112416504);
            bool2 r1 = bool2(true, true);
            TestUtils.AreEqual(a1 != b1, r1);

            uint a2 = (25967222);
            uint2 b2 = uint2(1761444475, 1844594536);
            bool2 r2 = bool2(true, true);
            TestUtils.AreEqual(a2 != b2, r2);

            uint a3 = (1199122922);
            uint2 b3 = uint2(634219279, 942501101);
            bool2 r3 = bool2(true, true);
            TestUtils.AreEqual(a3 != b3, r3);
        }

        [Test]
        public void uint2_operator_less_wide_wide()
        {
            uint2 a0 = uint2(1486550609, 1779244308);
            uint2 b0 = uint2(97842578, 536551311);
            bool2 r0 = bool2(false, false);
            TestUtils.AreEqual(a0 < b0, r0);

            uint2 a1 = uint2(1602148045, 1614085440);
            uint2 b1 = uint2(413528975, 1838293684);
            bool2 r1 = bool2(false, true);
            TestUtils.AreEqual(a1 < b1, r1);

            uint2 a2 = uint2(1975613414, 942838342);
            uint2 b2 = uint2(1283898480, 1456599961);
            bool2 r2 = bool2(false, true);
            TestUtils.AreEqual(a2 < b2, r2);

            uint2 a3 = uint2(1092279031, 373677431);
            uint2 b3 = uint2(1080278602, 529676676);
            bool2 r3 = bool2(false, true);
            TestUtils.AreEqual(a3 < b3, r3);
        }

        [Test]
        public void uint2_operator_less_wide_scalar()
        {
            uint2 a0 = uint2(796797557, 670113454);
            uint b0 = (746564682);
            bool2 r0 = bool2(false, true);
            TestUtils.AreEqual(a0 < b0, r0);

            uint2 a1 = uint2(933579492, 318174822);
            uint b1 = (278884514);
            bool2 r1 = bool2(false, false);
            TestUtils.AreEqual(a1 < b1, r1);

            uint2 a2 = uint2(1117630673, 1990922600);
            uint b2 = (741886928);
            bool2 r2 = bool2(false, false);
            TestUtils.AreEqual(a2 < b2, r2);

            uint2 a3 = uint2(1030849597, 1718582899);
            uint b3 = (1546212312);
            bool2 r3 = bool2(true, false);
            TestUtils.AreEqual(a3 < b3, r3);
        }

        [Test]
        public void uint2_operator_less_scalar_wide()
        {
            uint a0 = (186400299);
            uint2 b0 = uint2(1881344229, 813834467);
            bool2 r0 = bool2(true, true);
            TestUtils.AreEqual(a0 < b0, r0);

            uint a1 = (1254886626);
            uint2 b1 = uint2(1353590345, 1412343685);
            bool2 r1 = bool2(true, true);
            TestUtils.AreEqual(a1 < b1, r1);

            uint a2 = (1555571443);
            uint2 b2 = uint2(1540508298, 1735458634);
            bool2 r2 = bool2(false, true);
            TestUtils.AreEqual(a2 < b2, r2);

            uint a3 = (135888070);
            uint2 b3 = uint2(1643818742, 248291654);
            bool2 r3 = bool2(true, true);
            TestUtils.AreEqual(a3 < b3, r3);
        }

        [Test]
        public void uint2_operator_greater_wide_wide()
        {
            uint2 a0 = uint2(2087717754, 1725569452);
            uint2 b0 = uint2(85148514, 293632137);
            bool2 r0 = bool2(true, true);
            TestUtils.AreEqual(a0 > b0, r0);

            uint2 a1 = uint2(1298066182, 1693943616);
            uint2 b1 = uint2(1151128249, 409440398);
            bool2 r1 = bool2(true, true);
            TestUtils.AreEqual(a1 > b1, r1);

            uint2 a2 = uint2(1319019629, 70674491);
            uint2 b2 = uint2(1115020183, 1508500597);
            bool2 r2 = bool2(true, false);
            TestUtils.AreEqual(a2 > b2, r2);

            uint2 a3 = uint2(1042499725, 1002821508);
            uint2 b3 = uint2(1834583302, 1755218534);
            bool2 r3 = bool2(false, false);
            TestUtils.AreEqual(a3 > b3, r3);
        }

        [Test]
        public void uint2_operator_greater_wide_scalar()
        {
            uint2 a0 = uint2(1208626274, 239697208);
            uint b0 = (1715176566);
            bool2 r0 = bool2(false, false);
            TestUtils.AreEqual(a0 > b0, r0);

            uint2 a1 = uint2(1979453345, 1590192876);
            uint b1 = (1253474001);
            bool2 r1 = bool2(true, true);
            TestUtils.AreEqual(a1 > b1, r1);

            uint2 a2 = uint2(1487911635, 1662650098);
            uint b2 = (1673945595);
            bool2 r2 = bool2(false, false);
            TestUtils.AreEqual(a2 > b2, r2);

            uint2 a3 = uint2(222749855, 566635217);
            uint b3 = (1433540517);
            bool2 r3 = bool2(false, false);
            TestUtils.AreEqual(a3 > b3, r3);
        }

        [Test]
        public void uint2_operator_greater_scalar_wide()
        {
            uint a0 = (480938827);
            uint2 b0 = uint2(1824731899, 921496110);
            bool2 r0 = bool2(false, false);
            TestUtils.AreEqual(a0 > b0, r0);

            uint a1 = (586859044);
            uint2 b1 = uint2(946430596, 1231356727);
            bool2 r1 = bool2(false, false);
            TestUtils.AreEqual(a1 > b1, r1);

            uint a2 = (1390167458);
            uint2 b2 = uint2(1785807092, 28949024);
            bool2 r2 = bool2(false, true);
            TestUtils.AreEqual(a2 > b2, r2);

            uint a3 = (2037899283);
            uint2 b3 = uint2(595656760, 1778095771);
            bool2 r3 = bool2(true, true);
            TestUtils.AreEqual(a3 > b3, r3);
        }

        [Test]
        public void uint2_operator_less_equal_wide_wide()
        {
            uint2 a0 = uint2(154092149, 1515170149);
            uint2 b0 = uint2(77984380, 1712054191);
            bool2 r0 = bool2(false, true);
            TestUtils.AreEqual(a0 <= b0, r0);

            uint2 a1 = uint2(1083970332, 785807178);
            uint2 b1 = uint2(1566203809, 254834519);
            bool2 r1 = bool2(true, false);
            TestUtils.AreEqual(a1 <= b1, r1);

            uint2 a2 = uint2(1401094881, 310537627);
            uint2 b2 = uint2(450519938, 389457083);
            bool2 r2 = bool2(false, true);
            TestUtils.AreEqual(a2 <= b2, r2);

            uint2 a3 = uint2(868328962, 1990816725);
            uint2 b3 = uint2(1298669505, 207343167);
            bool2 r3 = bool2(true, false);
            TestUtils.AreEqual(a3 <= b3, r3);
        }

        [Test]
        public void uint2_operator_less_equal_wide_scalar()
        {
            uint2 a0 = uint2(1479531977, 1427983411);
            uint b0 = (2004563877);
            bool2 r0 = bool2(true, true);
            TestUtils.AreEqual(a0 <= b0, r0);

            uint2 a1 = uint2(415250630, 1112546666);
            uint b1 = (1245345407);
            bool2 r1 = bool2(true, true);
            TestUtils.AreEqual(a1 <= b1, r1);

            uint2 a2 = uint2(1072990632, 101048307);
            uint b2 = (1579912858);
            bool2 r2 = bool2(true, true);
            TestUtils.AreEqual(a2 <= b2, r2);

            uint2 a3 = uint2(1070894375, 1910488590);
            uint b3 = (509818792);
            bool2 r3 = bool2(false, false);
            TestUtils.AreEqual(a3 <= b3, r3);
        }

        [Test]
        public void uint2_operator_less_equal_scalar_wide()
        {
            uint a0 = (1899193992);
            uint2 b0 = uint2(915011820, 980913757);
            bool2 r0 = bool2(false, false);
            TestUtils.AreEqual(a0 <= b0, r0);

            uint a1 = (1337699683);
            uint2 b1 = uint2(1476321359, 1102143668);
            bool2 r1 = bool2(true, false);
            TestUtils.AreEqual(a1 <= b1, r1);

            uint a2 = (660493983);
            uint2 b2 = uint2(184664508, 381579707);
            bool2 r2 = bool2(false, false);
            TestUtils.AreEqual(a2 <= b2, r2);

            uint a3 = (1954892821);
            uint2 b3 = uint2(1295090571, 1440368586);
            bool2 r3 = bool2(false, false);
            TestUtils.AreEqual(a3 <= b3, r3);
        }

        [Test]
        public void uint2_operator_greater_equal_wide_wide()
        {
            uint2 a0 = uint2(8538378, 2131749726);
            uint2 b0 = uint2(903145828, 1697992986);
            bool2 r0 = bool2(false, true);
            TestUtils.AreEqual(a0 >= b0, r0);

            uint2 a1 = uint2(265427108, 523609761);
            uint2 b1 = uint2(1432491982, 169789504);
            bool2 r1 = bool2(false, true);
            TestUtils.AreEqual(a1 >= b1, r1);

            uint2 a2 = uint2(994991818, 839709564);
            uint2 b2 = uint2(1059357300, 1744255222);
            bool2 r2 = bool2(false, false);
            TestUtils.AreEqual(a2 >= b2, r2);

            uint2 a3 = uint2(101288202, 1886280970);
            uint2 b3 = uint2(1606584463, 1905414425);
            bool2 r3 = bool2(false, false);
            TestUtils.AreEqual(a3 >= b3, r3);
        }

        [Test]
        public void uint2_operator_greater_equal_wide_scalar()
        {
            uint2 a0 = uint2(2049236663, 182691143);
            uint b0 = (1061998015);
            bool2 r0 = bool2(true, false);
            TestUtils.AreEqual(a0 >= b0, r0);

            uint2 a1 = uint2(634973382, 439837565);
            uint b1 = (1197012109);
            bool2 r1 = bool2(false, false);
            TestUtils.AreEqual(a1 >= b1, r1);

            uint2 a2 = uint2(1367606469, 351147187);
            uint b2 = (1108037359);
            bool2 r2 = bool2(true, false);
            TestUtils.AreEqual(a2 >= b2, r2);

            uint2 a3 = uint2(1898505669, 1998610091);
            uint b3 = (411667685);
            bool2 r3 = bool2(true, true);
            TestUtils.AreEqual(a3 >= b3, r3);
        }

        [Test]
        public void uint2_operator_greater_equal_scalar_wide()
        {
            uint a0 = (1859811087);
            uint2 b0 = uint2(1070365918, 1783869452);
            bool2 r0 = bool2(true, true);
            TestUtils.AreEqual(a0 >= b0, r0);

            uint a1 = (830091760);
            uint2 b1 = uint2(377157428, 327199016);
            bool2 r1 = bool2(true, true);
            TestUtils.AreEqual(a1 >= b1, r1);

            uint a2 = (90384229);
            uint2 b2 = uint2(1154649706, 376758501);
            bool2 r2 = bool2(false, false);
            TestUtils.AreEqual(a2 >= b2, r2);

            uint a3 = (1079802834);
            uint2 b3 = uint2(2054742898, 1328349472);
            bool2 r3 = bool2(false, false);
            TestUtils.AreEqual(a3 >= b3, r3);
        }

        [Test]
        public void uint2_operator_add_wide_wide()
        {
            uint2 a0 = uint2(2135171378, 21433296);
            uint2 b0 = uint2(1013431952, 366718162);
            uint2 r0 = uint2(3148603330, 388151458);
            TestUtils.AreEqual(a0 + b0, r0);

            uint2 a1 = uint2(1954723494, 683604307);
            uint2 b1 = uint2(359290756, 1393163294);
            uint2 r1 = uint2(2314014250, 2076767601);
            TestUtils.AreEqual(a1 + b1, r1);

            uint2 a2 = uint2(1054212315, 1762680995);
            uint2 b2 = uint2(1962236872, 1263270041);
            uint2 r2 = uint2(3016449187, 3025951036);
            TestUtils.AreEqual(a2 + b2, r2);

            uint2 a3 = uint2(1963655852, 1257853062);
            uint2 b3 = uint2(1862666629, 1077447887);
            uint2 r3 = uint2(3826322481, 2335300949);
            TestUtils.AreEqual(a3 + b3, r3);
        }

        [Test]
        public void uint2_operator_add_wide_scalar()
        {
            uint2 a0 = uint2(665815972, 1783729250);
            uint b0 = (1334043849);
            uint2 r0 = uint2(1999859821, 3117773099);
            TestUtils.AreEqual(a0 + b0, r0);

            uint2 a1 = uint2(1591678394, 977850224);
            uint b1 = (1284528538);
            uint2 r1 = uint2(2876206932, 2262378762);
            TestUtils.AreEqual(a1 + b1, r1);

            uint2 a2 = uint2(71069732, 1200356017);
            uint b2 = (1138577680);
            uint2 r2 = uint2(1209647412, 2338933697);
            TestUtils.AreEqual(a2 + b2, r2);

            uint2 a3 = uint2(565982008, 1088001167);
            uint b3 = (1246759684);
            uint2 r3 = uint2(1812741692, 2334760851);
            TestUtils.AreEqual(a3 + b3, r3);
        }

        [Test]
        public void uint2_operator_add_scalar_wide()
        {
            uint a0 = (359966320);
            uint2 b0 = uint2(2146146202, 767103309);
            uint2 r0 = uint2(2506112522, 1127069629);
            TestUtils.AreEqual(a0 + b0, r0);

            uint a1 = (851002415);
            uint2 b1 = uint2(311531406, 1491262941);
            uint2 r1 = uint2(1162533821, 2342265356);
            TestUtils.AreEqual(a1 + b1, r1);

            uint a2 = (1016891373);
            uint2 b2 = uint2(1954228994, 1671335850);
            uint2 r2 = uint2(2971120367, 2688227223);
            TestUtils.AreEqual(a2 + b2, r2);

            uint a3 = (727870747);
            uint2 b3 = uint2(551151834, 1390261152);
            uint2 r3 = uint2(1279022581, 2118131899);
            TestUtils.AreEqual(a3 + b3, r3);
        }

        [Test]
        public void uint2_operator_sub_wide_wide()
        {
            uint2 a0 = uint2(1410318491, 1097280168);
            uint2 b0 = uint2(1315897366, 799052018);
            uint2 r0 = uint2(94421125, 298228150);
            TestUtils.AreEqual(a0 - b0, r0);

            uint2 a1 = uint2(1827039044, 28881338);
            uint2 b1 = uint2(1580289673, 1094686261);
            uint2 r1 = uint2(246749371, 3229162373);
            TestUtils.AreEqual(a1 - b1, r1);

            uint2 a2 = uint2(328720965, 875487868);
            uint2 b2 = uint2(1954325726, 1197734816);
            uint2 r2 = uint2(2669362535, 3972720348);
            TestUtils.AreEqual(a2 - b2, r2);

            uint2 a3 = uint2(212936325, 231977215);
            uint2 b3 = uint2(229886366, 915679176);
            uint2 r3 = uint2(4278017255, 3611265335);
            TestUtils.AreEqual(a3 - b3, r3);
        }

        [Test]
        public void uint2_operator_sub_wide_scalar()
        {
            uint2 a0 = uint2(1508669340, 1594795463);
            uint b0 = (998008471);
            uint2 r0 = uint2(510660869, 596786992);
            TestUtils.AreEqual(a0 - b0, r0);

            uint2 a1 = uint2(266707545, 287705008);
            uint b1 = (643102647);
            uint2 r1 = uint2(3918572194, 3939569657);
            TestUtils.AreEqual(a1 - b1, r1);

            uint2 a2 = uint2(1475644328, 979450511);
            uint b2 = (1113286221);
            uint2 r2 = uint2(362358107, 4161131586);
            TestUtils.AreEqual(a2 - b2, r2);

            uint2 a3 = uint2(2082174113, 304369206);
            uint b3 = (1108005498);
            uint2 r3 = uint2(974168615, 3491331004);
            TestUtils.AreEqual(a3 - b3, r3);
        }

        [Test]
        public void uint2_operator_sub_scalar_wide()
        {
            uint a0 = (893369501);
            uint2 b0 = uint2(2051906184, 1699714311);
            uint2 r0 = uint2(3136430613, 3488622486);
            TestUtils.AreEqual(a0 - b0, r0);

            uint a1 = (442603706);
            uint2 b1 = uint2(1735141684, 274533585);
            uint2 r1 = uint2(3002429318, 168070121);
            TestUtils.AreEqual(a1 - b1, r1);

            uint a2 = (811580259);
            uint2 b2 = uint2(1196354320, 1524097023);
            uint2 r2 = uint2(3910193235, 3582450532);
            TestUtils.AreEqual(a2 - b2, r2);

            uint a3 = (533621527);
            uint2 b3 = uint2(2080845793, 143958837);
            uint2 r3 = uint2(2747743030, 389662690);
            TestUtils.AreEqual(a3 - b3, r3);
        }

        [Test]
        public void uint2_operator_mul_wide_wide()
        {
            uint2 a0 = uint2(61417577, 219585476);
            uint2 b0 = uint2(578042444, 1620527213);
            uint2 r0 = uint2(2868103468, 1326150260);
            TestUtils.AreEqual(a0 * b0, r0);

            uint2 a1 = uint2(1362520891, 1511084277);
            uint2 b1 = uint2(200516468, 309339115);
            uint2 r1 = uint2(3304073148, 2972110823);
            TestUtils.AreEqual(a1 * b1, r1);

            uint2 a2 = uint2(1481211272, 58211871);
            uint2 b2 = uint2(542853019, 299467282);
            uint2 r2 = uint2(1973926232, 88546350);
            TestUtils.AreEqual(a2 * b2, r2);

            uint2 a3 = uint2(1459591173, 567624644);
            uint2 b3 = uint2(1479641221, 1428338601);
            uint2 r3 = uint2(3431047833, 3712145508);
            TestUtils.AreEqual(a3 * b3, r3);
        }

        [Test]
        public void uint2_operator_mul_wide_scalar()
        {
            uint2 a0 = uint2(871746615, 492532311);
            uint b0 = (442064533);
            uint2 r0 = uint2(338365955, 1081376419);
            TestUtils.AreEqual(a0 * b0, r0);

            uint2 a1 = uint2(570557670, 1610315153);
            uint b1 = (2142306629);
            uint2 r1 = uint2(2845194238, 310198549);
            TestUtils.AreEqual(a1 * b1, r1);

            uint2 a2 = uint2(1526163563, 257439514);
            uint b2 = (118471734);
            uint2 r2 = uint2(3961756306, 4120426364);
            TestUtils.AreEqual(a2 * b2, r2);

            uint2 a3 = uint2(364291059, 1584938026);
            uint b3 = (1186560810);
            uint2 r3 = uint2(1029154526, 989596900);
            TestUtils.AreEqual(a3 * b3, r3);
        }

        [Test]
        public void uint2_operator_mul_scalar_wide()
        {
            uint a0 = (1152242766);
            uint2 b0 = uint2(1276636134, 2105929407);
            uint2 r0 = uint2(2165463060, 999811634);
            TestUtils.AreEqual(a0 * b0, r0);

            uint a1 = (499007702);
            uint2 b1 = uint2(124002565, 1956335172);
            uint2 r1 = uint2(1219156526, 400321752);
            TestUtils.AreEqual(a1 * b1, r1);

            uint a2 = (1288034953);
            uint2 b2 = uint2(832676555, 337389733);
            uint2 r2 = uint2(3193437347, 10961485);
            TestUtils.AreEqual(a2 * b2, r2);

            uint a3 = (99268757);
            uint2 b3 = uint2(2005055247, 2011389505);
            uint2 r3 = uint2(559764411, 2675872725);
            TestUtils.AreEqual(a3 * b3, r3);
        }

        [Test]
        public void uint2_operator_div_wide_wide()
        {
            uint2 a0 = uint2(333171510, 858154903);
            uint2 b0 = uint2(698897823, 440199998);
            uint2 r0 = uint2(0, 1);
            TestUtils.AreEqual(a0 / b0, r0);

            uint2 a1 = uint2(1181365836, 671357749);
            uint2 b1 = uint2(655557473, 1658534285);
            uint2 r1 = uint2(1, 0);
            TestUtils.AreEqual(a1 / b1, r1);

            uint2 a2 = uint2(1090606752, 803759420);
            uint2 b2 = uint2(2127220100, 315653188);
            uint2 r2 = uint2(0, 2);
            TestUtils.AreEqual(a2 / b2, r2);

            uint2 a3 = uint2(788404166, 296807814);
            uint2 b3 = uint2(1814290360, 992173243);
            uint2 r3 = uint2(0, 0);
            TestUtils.AreEqual(a3 / b3, r3);
        }

        [Test]
        public void uint2_operator_div_wide_scalar()
        {
            uint2 a0 = uint2(1433072926, 1073958635);
            uint b0 = (1434025872);
            uint2 r0 = uint2(0, 0);
            TestUtils.AreEqual(a0 / b0, r0);

            uint2 a1 = uint2(1195142312, 1274375693);
            uint b1 = (536596719);
            uint2 r1 = uint2(2, 2);
            TestUtils.AreEqual(a1 / b1, r1);

            uint2 a2 = uint2(464756346, 906504670);
            uint b2 = (806462546);
            uint2 r2 = uint2(0, 1);
            TestUtils.AreEqual(a2 / b2, r2);

            uint2 a3 = uint2(1380905136, 1196815948);
            uint b3 = (25493909);
            uint2 r3 = uint2(54, 46);
            TestUtils.AreEqual(a3 / b3, r3);
        }

        [Test]
        public void uint2_operator_div_scalar_wide()
        {
            uint a0 = (519165704);
            uint2 b0 = uint2(1295178177, 775214121);
            uint2 r0 = uint2(0, 0);
            TestUtils.AreEqual(a0 / b0, r0);

            uint a1 = (467772046);
            uint2 b1 = uint2(1156881598, 310396565);
            uint2 r1 = uint2(0, 1);
            TestUtils.AreEqual(a1 / b1, r1);

            uint a2 = (759759959);
            uint2 b2 = uint2(243837702, 1616314235);
            uint2 r2 = uint2(3, 0);
            TestUtils.AreEqual(a2 / b2, r2);

            uint a3 = (1053470225);
            uint2 b3 = uint2(1320630160, 378773841);
            uint2 r3 = uint2(0, 2);
            TestUtils.AreEqual(a3 / b3, r3);
        }

        [Test]
        public void uint2_operator_mod_wide_wide()
        {
            uint2 a0 = uint2(258342924, 1454754891);
            uint2 b0 = uint2(1990080167, 1197348066);
            uint2 r0 = uint2(258342924, 257406825);
            TestUtils.AreEqual(a0 % b0, r0);

            uint2 a1 = uint2(723352342, 1981431473);
            uint2 b1 = uint2(651970512, 1659454050);
            uint2 r1 = uint2(71381830, 321977423);
            TestUtils.AreEqual(a1 % b1, r1);

            uint2 a2 = uint2(531756042, 716993627);
            uint2 b2 = uint2(241005212, 1866255454);
            uint2 r2 = uint2(49745618, 716993627);
            TestUtils.AreEqual(a2 % b2, r2);

            uint2 a3 = uint2(1667903349, 1331097004);
            uint2 b3 = uint2(1440101415, 595220963);
            uint2 r3 = uint2(227801934, 140655078);
            TestUtils.AreEqual(a3 % b3, r3);
        }

        [Test]
        public void uint2_operator_mod_wide_scalar()
        {
            uint2 a0 = uint2(560988938, 629524514);
            uint b0 = (1156862367);
            uint2 r0 = uint2(560988938, 629524514);
            TestUtils.AreEqual(a0 % b0, r0);

            uint2 a1 = uint2(767711194, 1399805893);
            uint b1 = (434281967);
            uint2 r1 = uint2(333429227, 96959992);
            TestUtils.AreEqual(a1 % b1, r1);

            uint2 a2 = uint2(792916846, 598661916);
            uint b2 = (1663690927);
            uint2 r2 = uint2(792916846, 598661916);
            TestUtils.AreEqual(a2 % b2, r2);

            uint2 a3 = uint2(1776636144, 1743722161);
            uint b3 = (1287035793);
            uint2 r3 = uint2(489600351, 456686368);
            TestUtils.AreEqual(a3 % b3, r3);
        }

        [Test]
        public void uint2_operator_mod_scalar_wide()
        {
            uint a0 = (933347930);
            uint2 b0 = uint2(549923387, 243114953);
            uint2 r0 = uint2(383424543, 204003071);
            TestUtils.AreEqual(a0 % b0, r0);

            uint a1 = (1884274390);
            uint2 b1 = uint2(1428033594, 655531454);
            uint2 r1 = uint2(456240796, 573211482);
            TestUtils.AreEqual(a1 % b1, r1);

            uint a2 = (1622674954);
            uint2 b2 = uint2(1107563514, 1614111094);
            uint2 r2 = uint2(515111440, 8563860);
            TestUtils.AreEqual(a2 % b2, r2);

            uint a3 = (914801920);
            uint2 b3 = uint2(1432263179, 564431096);
            uint2 r3 = uint2(914801920, 350370824);
            TestUtils.AreEqual(a3 % b3, r3);
        }

        [Test]
        public void uint2_operator_plus()
        {
            uint2 a0 = uint2(195392567, 222719748);
            uint2 r0 = uint2(195392567, 222719748);
            TestUtils.AreEqual(+a0, r0);

            uint2 a1 = uint2(1002351013, 109389333);
            uint2 r1 = uint2(1002351013, 109389333);
            TestUtils.AreEqual(+a1, r1);

            uint2 a2 = uint2(1515950277, 291471785);
            uint2 r2 = uint2(1515950277, 291471785);
            TestUtils.AreEqual(+a2, r2);

            uint2 a3 = uint2(264115671, 195779102);
            uint2 r3 = uint2(264115671, 195779102);
            TestUtils.AreEqual(+a3, r3);
        }

        [Test]
        public void uint2_operator_neg()
        {
            uint2 a0 = uint2(1385088677, 94114564);
            uint2 r0 = uint2(2909878619, 4200852732);
            TestUtils.AreEqual(-a0, r0);

            uint2 a1 = uint2(1350664872, 2067164853);
            uint2 r1 = uint2(2944302424, 2227802443);
            TestUtils.AreEqual(-a1, r1);

            uint2 a2 = uint2(218122493, 270553961);
            uint2 r2 = uint2(4076844803, 4024413335);
            TestUtils.AreEqual(-a2, r2);

            uint2 a3 = uint2(446497525, 1928771252);
            uint2 r3 = uint2(3848469771, 2366196044);
            TestUtils.AreEqual(-a3, r3);
        }

        [Test]
        public void uint2_operator_prefix_inc()
        {
            uint2 a0 = uint2(780471723, 954741756);
            uint2 r0 = uint2(780471724, 954741757);
            TestUtils.AreEqual(++a0, r0);

            uint2 a1 = uint2(272723451, 606294309);
            uint2 r1 = uint2(272723452, 606294310);
            TestUtils.AreEqual(++a1, r1);

            uint2 a2 = uint2(1514814550, 444189162);
            uint2 r2 = uint2(1514814551, 444189163);
            TestUtils.AreEqual(++a2, r2);

            uint2 a3 = uint2(1833356994, 1348278302);
            uint2 r3 = uint2(1833356995, 1348278303);
            TestUtils.AreEqual(++a3, r3);
        }

        [Test]
        public void uint2_operator_postfix_inc()
        {
            uint2 a0 = uint2(241865086, 2145821641);
            uint2 r0 = uint2(241865086, 2145821641);
            TestUtils.AreEqual(a0++, r0);

            uint2 a1 = uint2(1596166022, 529551271);
            uint2 r1 = uint2(1596166022, 529551271);
            TestUtils.AreEqual(a1++, r1);

            uint2 a2 = uint2(1656767229, 1213663244);
            uint2 r2 = uint2(1656767229, 1213663244);
            TestUtils.AreEqual(a2++, r2);

            uint2 a3 = uint2(1425753247, 389844650);
            uint2 r3 = uint2(1425753247, 389844650);
            TestUtils.AreEqual(a3++, r3);
        }

        [Test]
        public void uint2_operator_prefix_dec()
        {
            uint2 a0 = uint2(1331961415, 1612382200);
            uint2 r0 = uint2(1331961414, 1612382199);
            TestUtils.AreEqual(--a0, r0);

            uint2 a1 = uint2(1401591249, 1757136546);
            uint2 r1 = uint2(1401591248, 1757136545);
            TestUtils.AreEqual(--a1, r1);

            uint2 a2 = uint2(1895858159, 578341664);
            uint2 r2 = uint2(1895858158, 578341663);
            TestUtils.AreEqual(--a2, r2);

            uint2 a3 = uint2(1336115745, 1169239112);
            uint2 r3 = uint2(1336115744, 1169239111);
            TestUtils.AreEqual(--a3, r3);
        }

        [Test]
        public void uint2_operator_postfix_dec()
        {
            uint2 a0 = uint2(1870005937, 1708534798);
            uint2 r0 = uint2(1870005937, 1708534798);
            TestUtils.AreEqual(a0--, r0);

            uint2 a1 = uint2(704493460, 2060593573);
            uint2 r1 = uint2(704493460, 2060593573);
            TestUtils.AreEqual(a1--, r1);

            uint2 a2 = uint2(228744869, 1818778351);
            uint2 r2 = uint2(228744869, 1818778351);
            TestUtils.AreEqual(a2--, r2);

            uint2 a3 = uint2(1015253889, 1401191870);
            uint2 r3 = uint2(1015253889, 1401191870);
            TestUtils.AreEqual(a3--, r3);
        }

        [Test]
        public void uint2_operator_bitwise_and_wide_wide()
        {
            uint2 a0 = uint2(1055241304, 859321394);
            uint2 b0 = uint2(749698416, 748105424);
            uint2 r0 = uint2(749024336, 537928720);
            TestUtils.AreEqual(a0 & b0, r0);

            uint2 a1 = uint2(1088358961, 2090949513);
            uint2 b1 = uint2(1842764797, 9990577);
            uint2 r1 = uint2(1087769137, 8409473);
            TestUtils.AreEqual(a1 & b1, r1);

            uint2 a2 = uint2(300561740, 651904515);
            uint2 b2 = uint2(1989102105, 1666634755);
            uint2 r2 = uint2(277479432, 575816707);
            TestUtils.AreEqual(a2 & b2, r2);

            uint2 a3 = uint2(1331035868, 2012752753);
            uint2 b3 = uint2(58116798, 868036607);
            uint2 r3 = uint2(55888540, 867707761);
            TestUtils.AreEqual(a3 & b3, r3);
        }

        [Test]
        public void uint2_operator_bitwise_and_wide_scalar()
        {
            uint2 a0 = uint2(1513158868, 284695609);
            uint b0 = (1173647397);
            uint2 r0 = uint2(1076916228, 15732769);
            TestUtils.AreEqual(a0 & b0, r0);

            uint2 a1 = uint2(734595037, 1601252476);
            uint b1 = (1566510707);
            uint2 r1 = uint2(155780689, 1565592688);
            TestUtils.AreEqual(a1 & b1, r1);

            uint2 a2 = uint2(84213838, 430856908);
            uint b2 = (314333543);
            uint2 r2 = uint2(262214, 279730244);
            TestUtils.AreEqual(a2 & b2, r2);

            uint2 a3 = uint2(753481263, 1619794917);
            uint b3 = (327392481);
            uint2 r3 = uint2(8459297, 8394977);
            TestUtils.AreEqual(a3 & b3, r3);
        }

        [Test]
        public void uint2_operator_bitwise_and_scalar_wide()
        {
            uint a0 = (477163326);
            uint2 b0 = uint2(110453385, 703240362);
            uint2 r0 = uint2(68182536, 140544042);
            TestUtils.AreEqual(a0 & b0, r0);

            uint a1 = (2075630560);
            uint2 b1 = uint2(1884904031, 1734899436);
            uint2 r1 = uint2(1880169024, 1663046368);
            TestUtils.AreEqual(a1 & b1, r1);

            uint a2 = (947945203);
            uint2 b2 = uint2(1664399051, 458879298);
            uint2 r2 = uint2(536884931, 402681922);
            TestUtils.AreEqual(a2 & b2, r2);

            uint a3 = (158491426);
            uint2 b3 = uint2(79772356, 2054527944);
            uint2 r3 = uint2(4203008, 141558528);
            TestUtils.AreEqual(a3 & b3, r3);
        }

        [Test]
        public void uint2_operator_bitwise_or_wide_wide()
        {
            uint2 a0 = uint2(1920951869, 1750772852);
            uint2 b0 = uint2(214585254, 275414367);
            uint2 r0 = uint2(2130669503, 2021326207);
            TestUtils.AreEqual(a0 | b0, r0);

            uint2 a1 = uint2(1420019191, 732977093);
            uint2 b1 = uint2(858759675, 190211455);
            uint2 r1 = uint2(2008016383, 737572863);
            TestUtils.AreEqual(a1 | b1, r1);

            uint2 a2 = uint2(1169579447, 229437930);
            uint2 b2 = uint2(1218691723, 2129565457);
            uint2 r2 = uint2(1303895999, 2146367483);
            TestUtils.AreEqual(a2 | b2, r2);

            uint2 a3 = uint2(1966721348, 2039812323);
            uint2 b3 = uint2(1862809466, 495728846);
            uint2 r3 = uint2(2134507390, 2107455727);
            TestUtils.AreEqual(a3 | b3, r3);
        }

        [Test]
        public void uint2_operator_bitwise_or_wide_scalar()
        {
            uint2 a0 = uint2(1295304853, 1307252624);
            uint b0 = (1305527136);
            uint2 r0 = uint2(1307888629, 1308350448);
            TestUtils.AreEqual(a0 | b0, r0);

            uint2 a1 = uint2(350194630, 1774824542);
            uint b1 = (1128063578);
            uint2 r1 = uint2(1476389854, 1811803742);
            TestUtils.AreEqual(a1 | b1, r1);

            uint2 a2 = uint2(2085245467, 999162350);
            uint b2 = (1988423804);
            uint2 r2 = uint2(2127491711, 2140074494);
            TestUtils.AreEqual(a2 | b2, r2);

            uint2 a3 = uint2(2110327307, 341855232);
            uint b3 = (1050875188);
            uint2 r3 = uint2(2146115391, 1055087924);
            TestUtils.AreEqual(a3 | b3, r3);
        }

        [Test]
        public void uint2_operator_bitwise_or_scalar_wide()
        {
            uint a0 = (1768627592);
            uint2 b0 = uint2(1849658052, 1759912154);
            uint2 r0 = uint2(1870634956, 1777286618);
            TestUtils.AreEqual(a0 | b0, r0);

            uint a1 = (729979455);
            uint2 b1 = uint2(975926310, 1987690876);
            uint2 r1 = uint2(1001127487, 2147212159);
            TestUtils.AreEqual(a1 | b1, r1);

            uint a2 = (1809506714);
            uint2 b2 = uint2(1160695341, 408963395);
            uint2 r2 = uint2(1878978495, 2080041435);
            TestUtils.AreEqual(a2 | b2, r2);

            uint a3 = (314072711);
            uint2 b3 = uint2(1560007537, 1709003416);
            uint2 r3 = uint2(1593565175, 2013093535);
            TestUtils.AreEqual(a3 | b3, r3);
        }

        [Test]
        public void uint2_operator_bitwise_xor_wide_wide()
        {
            uint2 a0 = uint2(1843655608, 362425681);
            uint2 b0 = uint2(1422803441, 1234691140);
            uint2 r0 = uint2(959296073, 1544407317);
            TestUtils.AreEqual(a0 ^ b0, r0);

            uint2 a1 = uint2(640805534, 1342040268);
            uint2 b1 = uint2(119589253, 1101464929);
            uint2 r1 = uint2(554771227, 240840109);
            TestUtils.AreEqual(a1 ^ b1, r1);

            uint2 a2 = uint2(945678755, 980321850);
            uint2 b2 = uint2(121896337, 1880109018);
            uint2 r2 = uint2(1058935858, 1249815008);
            TestUtils.AreEqual(a2 ^ b2, r2);

            uint2 a3 = uint2(390165019, 1682422658);
            uint2 b3 = uint2(1000210266, 1718938232);
            uint2 r3 = uint2(752651585, 36917754);
            TestUtils.AreEqual(a3 ^ b3, r3);
        }

        [Test]
        public void uint2_operator_bitwise_xor_wide_scalar()
        {
            uint2 a0 = uint2(169345668, 176087064);
            uint b0 = (354464228);
            uint2 r0 = uint2(523809632, 526275068);
            TestUtils.AreEqual(a0 ^ b0, r0);

            uint2 a1 = uint2(2084362901, 193851255);
            uint b1 = (1663924004);
            uint2 r1 = uint2(521252273, 1755355219);
            TestUtils.AreEqual(a1 ^ b1, r1);

            uint2 a2 = uint2(517476661, 863800783);
            uint b2 = (905336222);
            uint2 r2 = uint2(724452523, 109762641);
            TestUtils.AreEqual(a2 ^ b2, r2);

            uint2 a3 = uint2(365003873, 954341348);
            uint b3 = (835837496);
            uint2 r3 = uint2(605051481, 154388956);
            TestUtils.AreEqual(a3 ^ b3, r3);
        }

        [Test]
        public void uint2_operator_bitwise_xor_scalar_wide()
        {
            uint a0 = (1361775641);
            uint2 b0 = uint2(1452773578, 459050892);
            uint2 r0 = uint2(129801427, 1249346453);
            TestUtils.AreEqual(a0 ^ b0, r0);

            uint a1 = (1986218254);
            uint2 b1 = uint2(1689037698, 999278344);
            uint2 r1 = uint2(315613836, 1307348486);
            TestUtils.AreEqual(a1 ^ b1, r1);

            uint a2 = (1229114508);
            uint2 b2 = uint2(1087843538, 733699740);
            uint2 r2 = uint2(160817246, 1660523024);
            TestUtils.AreEqual(a2 ^ b2, r2);

            uint a3 = (106187872);
            uint2 b3 = uint2(1533632738, 1107817672);
            uint2 r3 = uint2(1564290178, 1146337448);
            TestUtils.AreEqual(a3 ^ b3, r3);
        }

        [Test]
        public void uint2_operator_left_shift()
        {
            uint2 a0 = uint2(1129100049, 829482269);
            int b0 = (218351941);
            uint2 r0 = uint2(1771463200, 773628832);
            TestUtils.AreEqual(a0 << b0, r0);

            uint2 a1 = uint2(1571297368, 1872142968);
            int b1 = (443753193);
            uint2 r1 = uint2(1345368064, 759492608);
            TestUtils.AreEqual(a1 << b1, r1);

            uint2 a2 = uint2(249554593, 980302862);
            int b2 = (892627436);
            uint2 r2 = uint2(4268363776, 3821068288);
            TestUtils.AreEqual(a2 << b2, r2);

            uint2 a3 = uint2(62369727, 1271350845);
            int b3 = (849916599);
            uint2 r3 = uint2(3749707776, 511705088);
            TestUtils.AreEqual(a3 << b3, r3);
        }

        [Test]
        public void uint2_operator_right_shift()
        {
            uint2 a0 = uint2(809126085, 908563670);
            int b0 = (994800051);
            uint2 r0 = uint2(1543, 1732);
            TestUtils.AreEqual(a0 >> b0, r0);

            uint2 a1 = uint2(763568837, 1174507510);
            int b1 = (1986717290);
            uint2 r1 = uint2(745672, 1146979);
            TestUtils.AreEqual(a1 >> b1, r1);

            uint2 a2 = uint2(646821842, 390811632);
            int b2 = (1242726074);
            uint2 r2 = uint2(9, 5);
            TestUtils.AreEqual(a2 >> b2, r2);

            uint2 a3 = uint2(1521420393, 102096936);
            int b3 = (1923166649);
            uint2 r3 = uint2(45, 3);
            TestUtils.AreEqual(a3 >> b3, r3);
        }

        [Test]
        public void uint2_operator_bitwise_not()
        {
            uint2 a0 = uint2(111796841, 603562399);
            uint2 r0 = uint2(4183170454, 3691404896);
            TestUtils.AreEqual(~a0, r0);

            uint2 a1 = uint2(745091931, 1110916612);
            uint2 r1 = uint2(3549875364, 3184050683);
            TestUtils.AreEqual(~a1, r1);

            uint2 a2 = uint2(381888399, 1506860135);
            uint2 r2 = uint2(3913078896, 2788107160);
            TestUtils.AreEqual(~a2, r2);

            uint2 a3 = uint2(771210628, 875964607);
            uint2 r3 = uint2(3523756667, 3419002688);
            TestUtils.AreEqual(~a3, r3);
        }

        [Test]
        public void uint2_shuffle_result_1()
        {
            uint2 a = uint2(0, 1);
            uint2 b = uint2(2, 3);

            TestUtils.AreEqual(shuffle(a, b, ShuffleComponent.LeftX), (0));
            TestUtils.AreEqual(shuffle(a, b, ShuffleComponent.LeftY), (1));
            TestUtils.AreEqual(shuffle(a, b, ShuffleComponent.RightX), (2));
            TestUtils.AreEqual(shuffle(a, b, ShuffleComponent.RightY), (3));
        }

        [Test]
        public void uint2_shuffle_result_2()
        {
            uint2 a = uint2(0, 1);
            uint2 b = uint2(2, 3);

            TestUtils.AreEqual(shuffle(a, b, ShuffleComponent.LeftX, ShuffleComponent.LeftX), uint2(0, 0));
            TestUtils.AreEqual(shuffle(a, b, ShuffleComponent.LeftY, ShuffleComponent.LeftX), uint2(1, 0));
            TestUtils.AreEqual(shuffle(a, b, ShuffleComponent.RightX, ShuffleComponent.LeftX), uint2(2, 0));
            TestUtils.AreEqual(shuffle(a, b, ShuffleComponent.RightY, ShuffleComponent.LeftX), uint2(3, 0));
            TestUtils.AreEqual(shuffle(a, b, ShuffleComponent.LeftX, ShuffleComponent.LeftY), uint2(0, 1));
            TestUtils.AreEqual(shuffle(a, b, ShuffleComponent.LeftY, ShuffleComponent.LeftY), uint2(1, 1));
            TestUtils.AreEqual(shuffle(a, b, ShuffleComponent.RightX, ShuffleComponent.LeftY), uint2(2, 1));
            TestUtils.AreEqual(shuffle(a, b, ShuffleComponent.RightY, ShuffleComponent.LeftY), uint2(3, 1));
            TestUtils.AreEqual(shuffle(a, b, ShuffleComponent.LeftX, ShuffleComponent.RightX), uint2(0, 2));
            TestUtils.AreEqual(shuffle(a, b, ShuffleComponent.LeftY, ShuffleComponent.RightX), uint2(1, 2));
            TestUtils.AreEqual(shuffle(a, b, ShuffleComponent.RightX, ShuffleComponent.RightX), uint2(2, 2));
            TestUtils.AreEqual(shuffle(a, b, ShuffleComponent.RightY, ShuffleComponent.RightX), uint2(3, 2));
            TestUtils.AreEqual(shuffle(a, b, ShuffleComponent.LeftX, ShuffleComponent.RightY), uint2(0, 3));
            TestUtils.AreEqual(shuffle(a, b, ShuffleComponent.LeftY, ShuffleComponent.RightY), uint2(1, 3));
            TestUtils.AreEqual(shuffle(a, b, ShuffleComponent.RightX, ShuffleComponent.RightY), uint2(2, 3));
            TestUtils.AreEqual(shuffle(a, b, ShuffleComponent.RightY, ShuffleComponent.RightY), uint2(3, 3));
        }

        [Test]
        public void uint2_shuffle_result_3()
        {
            uint2 a = uint2(0, 1);
            uint2 b = uint2(2, 3);

            TestUtils.AreEqual(shuffle(a, b, ShuffleComponent.RightX, ShuffleComponent.RightY, ShuffleComponent.RightY), uint3(2, 3, 3));
            TestUtils.AreEqual(shuffle(a, b, ShuffleComponent.RightX, ShuffleComponent.LeftX, ShuffleComponent.RightX), uint3(2, 0, 2));
            TestUtils.AreEqual(shuffle(a, b, ShuffleComponent.RightY, ShuffleComponent.LeftY, ShuffleComponent.RightY), uint3(3, 1, 3));
            TestUtils.AreEqual(shuffle(a, b, ShuffleComponent.LeftY, ShuffleComponent.LeftY, ShuffleComponent.LeftY), uint3(1, 1, 1));
            TestUtils.AreEqual(shuffle(a, b, ShuffleComponent.RightX, ShuffleComponent.LeftY, ShuffleComponent.RightY), uint3(2, 1, 3));
            TestUtils.AreEqual(shuffle(a, b, ShuffleComponent.LeftX, ShuffleComponent.RightY, ShuffleComponent.RightY), uint3(0, 3, 3));
            TestUtils.AreEqual(shuffle(a, b, ShuffleComponent.RightX, ShuffleComponent.LeftY, ShuffleComponent.RightY), uint3(2, 1, 3));
            TestUtils.AreEqual(shuffle(a, b, ShuffleComponent.RightY, ShuffleComponent.RightY, ShuffleComponent.LeftX), uint3(3, 3, 0));
            TestUtils.AreEqual(shuffle(a, b, ShuffleComponent.RightX, ShuffleComponent.RightX, ShuffleComponent.RightY), uint3(2, 2, 3));
            TestUtils.AreEqual(shuffle(a, b, ShuffleComponent.RightX, ShuffleComponent.RightX, ShuffleComponent.LeftX), uint3(2, 2, 0));
            TestUtils.AreEqual(shuffle(a, b, ShuffleComponent.LeftX, ShuffleComponent.LeftY, ShuffleComponent.LeftY), uint3(0, 1, 1));
            TestUtils.AreEqual(shuffle(a, b, ShuffleComponent.RightY, ShuffleComponent.RightX, ShuffleComponent.RightY), uint3(3, 2, 3));
            TestUtils.AreEqual(shuffle(a, b, ShuffleComponent.LeftX, ShuffleComponent.LeftY, ShuffleComponent.LeftY), uint3(0, 1, 1));
            TestUtils.AreEqual(shuffle(a, b, ShuffleComponent.RightY, ShuffleComponent.RightX, ShuffleComponent.RightX), uint3(3, 2, 2));
            TestUtils.AreEqual(shuffle(a, b, ShuffleComponent.LeftX, ShuffleComponent.LeftY, ShuffleComponent.RightY), uint3(0, 1, 3));
            TestUtils.AreEqual(shuffle(a, b, ShuffleComponent.RightY, ShuffleComponent.LeftY, ShuffleComponent.LeftY), uint3(3, 1, 1));
        }

        [Test]
        public void uint2_shuffle_result_4()
        {
            uint2 a = uint2(0, 1);
            uint2 b = uint2(2, 3);

            TestUtils.AreEqual(shuffle(a, b, ShuffleComponent.LeftX, ShuffleComponent.LeftX, ShuffleComponent.LeftY, ShuffleComponent.RightX), uint4(0, 0, 1, 2));
            TestUtils.AreEqual(shuffle(a, b, ShuffleComponent.RightX, ShuffleComponent.LeftY, ShuffleComponent.LeftX, ShuffleComponent.RightY), uint4(2, 1, 0, 3));
            TestUtils.AreEqual(shuffle(a, b, ShuffleComponent.RightY, ShuffleComponent.RightX, ShuffleComponent.RightY, ShuffleComponent.RightX), uint4(3, 2, 3, 2));
            TestUtils.AreEqual(shuffle(a, b, ShuffleComponent.RightX, ShuffleComponent.LeftX, ShuffleComponent.RightY, ShuffleComponent.RightY), uint4(2, 0, 3, 3));
            TestUtils.AreEqual(shuffle(a, b, ShuffleComponent.LeftX, ShuffleComponent.RightY, ShuffleComponent.LeftX, ShuffleComponent.RightX), uint4(0, 3, 0, 2));
            TestUtils.AreEqual(shuffle(a, b, ShuffleComponent.RightY, ShuffleComponent.LeftX, ShuffleComponent.RightY, ShuffleComponent.LeftY), uint4(3, 0, 3, 1));
            TestUtils.AreEqual(shuffle(a, b, ShuffleComponent.RightX, ShuffleComponent.RightX, ShuffleComponent.RightY, ShuffleComponent.LeftX), uint4(2, 2, 3, 0));
            TestUtils.AreEqual(shuffle(a, b, ShuffleComponent.RightX, ShuffleComponent.RightX, ShuffleComponent.LeftX, ShuffleComponent.RightY), uint4(2, 2, 0, 3));
            TestUtils.AreEqual(shuffle(a, b, ShuffleComponent.LeftX, ShuffleComponent.LeftY, ShuffleComponent.RightX, ShuffleComponent.LeftY), uint4(0, 1, 2, 1));
            TestUtils.AreEqual(shuffle(a, b, ShuffleComponent.RightX, ShuffleComponent.LeftY, ShuffleComponent.RightY, ShuffleComponent.RightY), uint4(2, 1, 3, 3));
            TestUtils.AreEqual(shuffle(a, b, ShuffleComponent.RightY, ShuffleComponent.RightY, ShuffleComponent.RightY, ShuffleComponent.LeftY), uint4(3, 3, 3, 1));
            TestUtils.AreEqual(shuffle(a, b, ShuffleComponent.RightX, ShuffleComponent.RightX, ShuffleComponent.LeftY, ShuffleComponent.LeftY), uint4(2, 2, 1, 1));
            TestUtils.AreEqual(shuffle(a, b, ShuffleComponent.RightY, ShuffleComponent.LeftX, ShuffleComponent.LeftX, ShuffleComponent.LeftX), uint4(3, 0, 0, 0));
            TestUtils.AreEqual(shuffle(a, b, ShuffleComponent.RightX, ShuffleComponent.RightX, ShuffleComponent.RightX, ShuffleComponent.RightY), uint4(2, 2, 2, 3));
            TestUtils.AreEqual(shuffle(a, b, ShuffleComponent.LeftX, ShuffleComponent.RightY, ShuffleComponent.LeftX, ShuffleComponent.RightY), uint4(0, 3, 0, 3));
            TestUtils.AreEqual(shuffle(a, b, ShuffleComponent.RightX, ShuffleComponent.LeftX, ShuffleComponent.LeftX, ShuffleComponent.LeftX), uint4(2, 0, 0, 0));
        }


    }
}
