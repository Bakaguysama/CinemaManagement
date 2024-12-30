CREATE DATABASE CINEMAMANAGEMENT

USE DATABASE CINEMAMANAGEMENT

SET DATEFORMAT dmy

CREATE TABLE NHANVIEN
(
MANV char(5) primary key,
HOTEN nvarchar(40),
NGAYVL smalldatetime,
SDT varchar(40),
GIOITINH nvarchar(3),
LUONG money,
CHUCVU nvarchar(40)
)

ALTER TABLE NHANVIEN
ADD CHECK (GIOITINH IN(N'Nam', N'Nữ'))

INSERT INTO NHANVIEN(MANV, HOTEN, NGAYVL, SDT, GIOITINH, LUONG, CHUCVU)
VALUES
('NV001', N'Khang', '04/12/2005', '123456', N'Nữ', 12000000, N'Quản Lí'),
('NV002', N'Michael', '05/12/2005', '324576', N'Nam', 1000000, N'Bán Hàng'),
('NV003', N'Quốc Huy', '7/7/1997', '000001', N'Nữ', 13000000, N'Quản Lí'),
('NV004', N'Khoa', '06/12/2005', '000002', N'Nam', 14000000, N'Bán Hàng'),
('NV005', N'Gia Huy', '05/05/1999', '000003', N'Nam', 15000000, N'Bán Hàng'),
('NV006', N'Tâm', '11/9/2001', '000004', N'Nam', 17000000, N'Bán Hàng'),
('NV007', N'Jack', '20/11/2001','000005', N'Nữ', 18000000, N'Bán Hàng'),
('NV008', N'Sơn Tùng', '22/12/2004', '000006', N'Nam', 1900000, N'Bán Hàng'),
('NV009', N'Thành Nam', '01/01/1998','000007', N'Nam', 20000000, N'Bán Hàng'),
('NV010', N'Thuỷ Tiên', '19/5/2000', '000008', N'Nữ', 11000000, N'Bán Hàng')

CREATE TABLE TAIKHOAN
(
TENTK nvarchar(40) primary key,
MATKHAU nvarchar(40),
MANV char(5) foreign key references NHANVIEN(MANV) 
)

INSERT INTO TAIKHOAN (TENTK, MATKHAU, MANV)
VALUES
('admin1', '12345', 'NV001'),
('admin2', '123456', 'NV003'),
('nhanvien1', '789', 'NV002'),
('nhanvien2', '1230', 'NV004'),
('nhanvien3', '1234', 'NV005'),
('nhanvien4', '2345', 'NV006'),
('nhanvien5', '111', 'NV007'),
('nhanvien6', '222', 'NV008'),
('nhanvien7', '333', 'NV009'),
('nhanvien8', '444', 'NV010')

CREATE TABLE SANPHAM
(
MASP char(5) primary key,
TENSP nvarchar(40),
LOAI nvarchar(40),
GIA money,
SOLUONG int DEFAULT 0
)

INSERT INTO SANPHAM (MASP, TENSP, LOAI, GIA)
VALUES
('SP001', 'Coca', N'Thức uống', 10000),
('SP002', N'Bắp rang', N'Thức ăn', 15000),
('SP003', 'Fanta', N'Thức uống', 10000),
('SP004', N'Que cay', N'Thức ăn', 5000),
('SP005', 'Sprite', N'Thức uống', 10000),
('SP006', N'Bắp rang phô mai', N'Thức ăn', 20000),
('SP007', 'Pepsi', N'Thức uống', 10000),
('SP008', N'Bắp rang bơ', N'Thức ăn', 20000),
('SP009', N'Nước lọc', N'Thức uống', 10000),
('SP010', N'Bánh Oshi', N'Thức ăn', 5000)

CREATE TABLE KHACHHANG 
(
MAKH char(5) primary key,
HOTEN nvarchar(40),
SDT int,
NGAYDK smalldatetime,
GIOITINH nvarchar(3)
)

ALTER TABLE KHACHHANG
ADD CHECK (GIOITINH IN (N'Nữ', N'Nam'))

INSERT INTO KHACHHANG(MAKH, HOTEN, SDT, NGAYDK, GIOITINH)
VALUES
('KH001', N'Lê Lưu Bách Đạt', 6969, '23/10/2024', N'Nam'),
('KH002', N'Trần Sư Toàn', 8386, '24/10/2024', N'Nữ'),
('KH003', N'Đỗ Đình Sang', 000009, '23/10/2024', N'Nam'),
('KH004', N'Đặng Ngọc Tài', 000010, '24/10/2024', N'Nữ'),
('KH005', N'Lê Viết Huy', 000011, '23/10/2024', N'Nam'),
('KH006', N'Đặng Thanh Phê', 000012, '24/10/2024', N'Nữ'),
('KH007', N'Nguyễn Hải Trung', 000013, '23/10/2024', N'Nam'),
('KH008', N'Nguyễn Linh Vương', 000014, '24/10/2024', N'Nữ'),
('KH009', N'Nguyễn Tuấn Thọ', 000015, '23/10/2024', N'Nam'),
('KH010', N'Bùi Văn Chu', 000016, '24/10/2024', N'Nữ')

CREATE TABLE HOADON
(
SOHD char(5) primary key,
MAKH char(5) foreign key references KHACHHANG(MAKH),
NGAYHD smalldatetime
)

INSERT INTO HOADON(SOHD, MAKH, NGAYHD)
VALUES
('HD001', 'KH001', '28/12/2024'),
('HD002', 'KH002', '26/12/2024'),
('HD003', 'KH003', '28/12/2024'),
('HD004', 'KH004', '26/12/2024'),
('HD005', 'KH005', '27/12/2024'),
('HD006', 'KH007', '27/12/2024'),
('HD007', 'KH006', '27/12/2024'),
('HD008', 'KH008', '26/12/2024'),
('HD009', 'KH009', '27/12/2024'),
('HD010', 'KH010', '28/12/2024')

CREATE TABLE PHIM
(
MAPHIM char(5) primary key,
TENPHIM nvarchar(40),
THOILUONG int,
THELOAI nvarchar(40),
DAODIEN nvarchar(40),
QUOCGIA nvarchar(40),
NAMPH int,
MOTA nvarchar(40),
TINHTRANG nvarchar(40),
GIANHAP money
)

ALTER TABLE PHIM
ADD CHECK (THOILUONG > 0)

ALTER TABLE PHIM
ADD CHECK (NAMPH > 0)

ALTER TABLE PHIM
ADD CHECK (TINHTRANG IN(N'Đang chiếu', N'Ngừng chiếu'))

INSERT INTO PHIM(MAPHIM, TENPHIM, THOILUONG, THELOAI, DAODIEN, QUOCGIA, NAMPH, MOTA, TINHTRANG, GIANHAP)
VALUES
('PH001', 'Bocchi The Rock', 120, 'Anime', 'Keiichiro Saito', N'Nhật Bản', 2024, N'Bocchi kẹo con', N'Đang chiếu', 20000000),
('PH002', 'Venom 3', 110, 'Sci-fi', 'Kelly Marcel', N'Mỹ', 2024, N'Venom đi lạc', N'Ngừng chiếu', 300000000),
('PH003', N'Cười Xuyên Biên Giới', 113, N'Hài', 'Keiichiro Saito', N'Hàn Quốc', 2024, N'Hài Hàn Quốc', N'Đang chiếu', 40000000),
('PH004', N'Sắc màu của cảm xúc', 102, 'Anime', 'Naoko Yamada', N'Nhật Bản', 2024, N'Tình bạn go brr', N'Ngừng chiếu', 500000000),
('PH005', N'Mật mã đỏ', 125, N'Hành Động', 'Jake Kasdan', N'Mỹ', 2024, N'Ông già Noel bị bắt cóc', N'Đang chiếu', 60000000),
('PH006', N'Đôi bạn học yêu', 118, N'Tình Cảm', 'E.Oni', N'Hàn Quốc', 2024, N'Đôi bạn ngỗ nghịch', N'Ngừng chiếu', 700000000),
('PH007', N'Hồn ma theo đuổi', 97, N'Kinh Dị', 'Banjong Pisanthanakun', N'Thái Lan', 2024, N'Gây tai nạn bỏ trốn', N'Đang chiếu', 80000000),
('PH008', N'Giải cứu anh "Thầy"', 98, N'Hài', N'Nguyễn Phi Phi Anh', N'Việt Nam', 2024, N'Hài Việt Nam', N'Ngừng chiếu', 900000000),
('PH009', N'Kẻ đóng thế', 114, N'Hành Động', N'Lương Quán Nghiêu', N'Trung Quốc', 2024, N'Đạo diễn hết thời', N'Đang chiếu', 10000000),
('PH010', N'Học viện anh hùng', 110, 'Anime', 'Tensai Okamura', N'Nhật Bản', 2024, N'Deku-kun', N'Ngừng chiếu', 110000000)

CREATE TABLE RAPCHIEUPHIM
(
MARAP char(5) primary key,
TENRAP nvarchar(40),
SOCHO int
)

ALTER TABLE RAPCHIEUPHIM
ADD CHECK (SOCHO > 0)

INSERT INTO RAPCHIEUPHIM(MARAP, TENRAP, SOCHO)
VALUES
('RP001', N'Rap số 1', 200),
('RP002', N'Rạp số 2', 300),
('RP003', N'Rap số 3', 150),
('RP004', N'Rạp số 4', 400),
('RP005', N'Rap số 5', 100),
('RP006', N'Rạp số 6', 350),
('RP007', N'Rap số 7', 250),
('RP008', N'Rạp số 8', 450),
('RP009', N'Rap số 9', 230),
('RP010', N'Rạp số 10', 312)

CREATE TABLE SUATCHIEU
(
MASUAT char(5) primary key,
GIOCHIEU date,
MARAP char(5) foreign key references RAPCHIEUPHIM(MARAP),
MAPHIM char(5) foreign key references PHIM(MAPHIM)
)

INSERT INTO SUATCHIEU(MASUAT, GIOCHIEU, MARAP, MAPHIM)
VALUES
('SU001', '27/12/2024', 'RP001', 'PH001'),
('SU002', '27/12/2024', 'RP002', 'PH002'),
('SU003', '27/12/2024', 'RP003', 'PH003'),
('SU004', '28/12/2024', 'RP004', 'PH004'),
('SU005', '28/12/2024', 'RP005', 'PH005'),
('SU006', '29/12/2024', 'RP006', 'PH006'),
('SU007', '29/12/2024', 'RP007', 'PH007'),
('SU008', '30/12/2024', 'RP008', 'PH008'),
('SU009', '30/12/2024', 'RP009', 'PH009'),
('SU010', '31/12/2024', 'RP010', 'PH010');

CREATE TABLE NHAPSP
(
MANV char(5) not null foreign key references NHANVIEN(MANV),
MASP char(5) not null foreign key references SANPHAM(MASP),
SOLUONG int
)

ALTER TABLE NHAPSP
ADD primary key (MANV,MASP)

ALTER TABLE NHAPSP
ADD CHECK (SOLUONG > 0)

INSERT INTO NHAPSP(MANV, MASP, SOLUONG)
VALUES
('NV001', 'SP001', 10),
('NV002', 'SP002', 20),
('NV003', 'SP003', 30),
('NV004', 'SP004', 40),
('NV005', 'SP005', 20),
('NV006', 'SP006', 30),
('NV007', 'SP007', 10),
('NV008', 'SP008', 20),
('NV009', 'SP009', 30),
('NV010', 'SP010', 40)

CREATE TABLE VEXEMPHIM
(
MAVE char(5) primary key,
GIAVE money,
NGAYMUA smalldatetime,
SOGHE varchar(40),
MASUAT char(5) not null foreign key references SUATCHIEU(MASUAT),
MARAP char(5) not null foreign key references RAPCHIEUPHIM(MARAP),
MAPHIM char(5) not null foreign key references PHIM(MAPHIM)
)

INSERT INTO VEXEMPHIM (MAVE, GIAVE, NGAYMUA, SOGHE, MASUAT, MARAP, MAPHIM)
VALUES 
('VE001', 120000, '27/12/2024', 'A1', 'SU001', 'RP001', 'PH001'),
('VE002', 120000, '27/12/2024', 'B1', 'SU003', 'RP003', 'PH003'),
('VE003', 120000, '28/12/2024', 'C1', 'SU005', 'RP005', 'PH005'),
('VE004', 120000, '29/12/2024', 'D1', 'SU007', 'RP007', 'PH007'),
('VE005', 120000, '30/12/2024', 'E1', 'SU009', 'RP009', 'PH009'),
('VE006', 120000, '27/12/2024', 'F1', 'SU001', 'RP001', 'PH001'),
('VE007', 120000, '27/12/2024', 'G1', 'SU003', 'RP003', 'PH003'),
('VE008', 120000, '28/12/2024', 'H1', 'SU005', 'RP005', 'PH005'),
('VE009', 120000, '28/12/2024', 'I1', 'SU007', 'RP007', 'PH007'),
('VE010', 120000, '30/12/2024', 'J1', 'SU009', 'RP009', 'PH009')
SELECT * FROM VEXEMPHIM

CREATE TABLE CTHD_SP
(
SOHD char(5) not null foreign key references HOADON(SOHD),
MASP char(5) not null foreign key references SANPHAM(MASP),
SOSP int
)

ALTER TABLE CTHD_SP
ADD primary key (SOHD, MASP)

INSERT INTO CTHD_SP (SOHD, MASP, SOSP)
VALUES 
('HD001', 'SP001', 5),
('HD001', 'SP002', 2),
('HD002', 'SP003',1),
('HD002', 'SP004',4),
('HD003', 'SP006', 3),
('HD004', 'SP005', 2),
('HD005', 'SP007',4),
('HD006', 'SP008',1),
('HD007', 'SP009', 3),
('HD008', 'SP010', 2),
('HD009', 'SP001',2),
('HD010', 'SP002',5)

CREATE TABLE CTHD_VXP
(
SOHD char(5) not null foreign key references HOADON(SOHD),
MAVE char(5) not null foreign key references VEXEMPHIM(MAVE),
SOVE int
)

ALTER TABLE CTHD_VXP
ADD primary key (SOHD, MAVE)

INSERT INTO CTHD_VXP(SOHD, MAVE, SOVE)
VALUES 
('HD001', 'VE001', 3),
('HD001', 'VE002', 1),
('HD002', 'VE003',2),
('HD003', 'VE004', 1),
('HD004', 'VE005', 6),
('HD005', 'VE006',2),
('HD006', 'VE007', 4),
('HD007', 'VE008', 2),
('HD008', 'VE009',2),
('HD009', 'VE010', 3),
('HD010', 'VE005', 4),
('HD010', 'VE009',5)

CREATE TRIGGER trg_UpdateSanPhamStock ON CTHD_SP
AFTER INSERT 
AS
BEGIN
    UPDATE SANPHAM
    SET SOLUONG = SOLUONG - INSERTED.SOSP
    FROM SANPHAM
    INNER JOIN INSERTED
    ON SANPHAM.MASP = INSERTED.MASP

    IF EXISTS (
        SELECT 1
        FROM SANPHAM
        WHERE SOLUONG < 0
    )
    BEGIN
        RAISERROR ('So luong san pham khong the am!', 16, 1)
        ROLLBACK TRANSACTION
    END
END