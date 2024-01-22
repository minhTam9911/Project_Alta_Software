insert into StaffUsers(
    Id,
    Fullname,
    Email,
    [Password],
    PhoneNumber,
    SecurityCode,
    [Address],
    CreateBy,
    PhotoAvatar,
    PositionId,
    CreatedDate,
    IsStatus,
    AreaId
)
values(
    'e9453551-b291-4413-83ad-ff16252de986',
    'Super Admin',
    'minhtamceo1@gmail.com',
    '$2a$11$YDVnYl1krrIIQwCC62onhOVsMyiDlDrd.016ZArVU3.VHe/U.3DK', --abc123
    null,
    null,
    null,
    null,
    'avatar-default-icon.png',
    1,
    '2024-01-15T10:22:34',
    1,
    null
)go

