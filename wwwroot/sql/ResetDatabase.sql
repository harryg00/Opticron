TRUNCATE TABLE [dbo].[HTMLCards];

INSERT INTO [dbo].[HTMLCards] (title, image, description, link, category)
VALUES 
('New Products', '/res/Card1Image.png', 'Lorem ipsum dolor sit amet, id per dictas inermis. Eam odio modo cibo at. Purto dignissim euripidis eu mel, cu mel', '#', 'ProductInfo'),
('Field Events', '/res/Card2Image.png', 'Lorem ipsum dolor sit amet, id per dictas inermis. Eam odio modo cibo at. Purto dignissim euripidis eu mel, cu mel', '#', 'ProductInfo'),
('Latest News', '/res/Card3Image.png', 'Lorem ipsum dolor sit amet, id per dictas inermis. Eam odio modo cibo at. Purto dignissim euripidis eu mel, cu mel', '#', 'ProductInfo'),
('Gallery', '/res/Card4Image.png', 'Lorem ipsum dolor sit amet, id per dictas inermis. Eam odio modo cibo at. Purto dignissim euripidis eu mel, cu mel', '#', 'ProductInfo'),
(NULL, '/res/SOCard1Image.png', 'Discovery WP PC</br><b>£20 Cashback</b>', '#', 'SOCard'),
(NULL, '/res/SOCard3Image.png', 'IS 60 WP Fieldscope Kits</br><b>Save 25%</b>', '#', 'SOCard'),
(NULL, '/res/SOCard2Image.png', 'HR ED Fieldscopes</br><b>Free Digiscoping Kit</b>', '#', 'SOCard'),
(NULL, '/res/TopSliderPlaceholder.png', NULL, '#', 'TopSlider'),
(NULL, '/res/TopSliderPlaceholder.png', NULL, '#', 'TopSlider'),
(NULL, '/res/TopSliderPlaceholder.png', NULL, '#', 'TopSlider'),
('View All Offers', NULL, NULL, '#', 'SOCard'),
('Binoculars', '/res/Binoculars.png', NULL, '#', 'ProductCategory'),
('Compact Binoculars', '/res/CompactBinoculars.png', NULL, '#', 'ProductCategory'),
('Telescopes & Eyepieces', '/res/Telescope.png', NULL, '#', 'ProductCategory'),
('Observation & Marine', '/res/Observation.png', NULL, '#', 'ProductCategory'),
('Observation & Marine', '/res/Observation.png', NULL, '#', 'ProductCategory'),
('Site Map', NULL, NULL, '#', 'BreadcrumbText'),
('Terms', NULL, NULL, '#', 'BreadcrumbText'),
('Privacy Policy', NULL, NULL, '#', 'BreadcrumbText'),
('Site By Verto', NULL, NULL, '#', 'BreadcrumbText'),
('Facebook', '/res/FacebookLogo.png', NULL, '#', 'BreadcrumbIcon'),
('Twitter', '/res/TwitterLogo.png', NULL, '#', 'BreadcrumbIcon'),
('LinkedIn', '/res/LinkedInLogo.png', NULL, '#', 'BreadcrumbIcon'),
('YouTube', '/res/YouTubeLogo.png', NULL, '#', 'BreadcrumbIcon'),
('Google', '/res/GoogleLogo.png', NULL, '#', 'BreadcrumbIcon');