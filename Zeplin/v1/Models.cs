// ReSharper disable UnusedType.Global
// ReSharper disable InconsistentNaming
// ReSharper disable NotAccessedField.Global
// ReSharper disable IdentifierTypo
// ReSharper disable CommentTypo
namespace BricksBucket.Zeplin.v1
{
	#region Error Models

	/// <summary>
	/// <seealso href="https://docs.zeplin.dev/reference#error"/>
	/// </summary>
	[System.Serializable]
	public struct Error
	{
		/// <summary>
		/// A user readable descriptive message for the error.
		/// </summary>
		public string message;

		/// <summary>
		/// A detailed message describing the error. Optional.
		/// </summary>
		public string detail;

		/// <summary>
		/// The unique code for the error. Optional.
		/// </summary>
		public string code;
	}

	/// <summary>
	/// Collection of errors in bad request.
	/// </summary>
	[System.Serializable]
	public struct ErrorCollection
	{
		/// <summary>
		/// Collection of errors.
		/// </summary>
		public Error[] errors;
	}

	#endregion

	#region Users Models

	/// <summary>
	/// <seealso href="https://docs.zeplin.dev/reference#user"/>
	/// </summary>
	[System.Serializable]
	public struct User
	{
		/// <summary>
		/// User's unique id.
		/// </summary>
		public string id;

		/// <summary>
		/// Email of the user.
		/// </summary>
		public string email;

		/// <summary>
		/// Username of the user.
		/// </summary>
		public string username;

		/// <summary>
		/// Emotar of the user. Optional.
		/// </summary>
		public string emotar;

		/// <summary>
		/// Avatar of the user. Optional.
		/// </summary>
		public string avatar;
	}

	/// <summary>
	/// Role of a user.
	/// </summary>
	public enum Role
	{
		OWNER,
		ADMIN,
		USER,
		EDITOR,
		MEMBER,
		ALIEN
	}

	#endregion

	#region Authorization Models

	/// <summary>
	/// <seealso href="https://docs.zeplin.dev/reference#token_response"/>
	/// </summary>
	[System.Serializable]
	public struct TokenResponse
	{
		/// <summary>
		/// Access token that allows you to make requests to the API on behalf
		/// of a user.
		/// </summary>
		public string accessToke;

		/// <summary>
		/// Access token's lifetime in seconds.
		/// </summary>
		public float expiresIn;

		/// <summary>
		/// Refresh token that allows you to obtain access tokens.
		/// </summary>
		public string refreshToke;

		/// <summary>
		/// Refresh token's lifetime in seconds.
		/// </summary>
		public float refreshExpiresIn;

		/// <summary>
		/// Type of the token returned.
		/// </summary>
		public string tokenType;
	}

	#endregion

	#region Organizations Models

	/// <summary>
	/// <seealso href="https://docs.zeplin.dev/reference#organization"/>
	/// </summary>
	[System.Serializable]
	public struct Organization
	{
		/// <summary>
		/// Organization's unique id.
		/// </summary>
		public string id;

		/// <summary>
		/// Name of the user.
		/// </summary>
		public string name;

		/// <summary>
		/// URL of the organization's logo. Optional.
		/// </summary>
		public string logo;

		/// <summary>
		/// Members of the organization (Does not exist on the response when
		/// user is restricted in organization). Optional.
		/// </summary>
		public OrganizationMember[] members;
	}

	/// <summary>
	/// <see href="https://docs.zeplin.dev/reference#organization_summary"/>
	/// </summary>
	[System.Serializable]
	public struct OrganizationSummary
	{
		/// <summary>
		/// Organization's unique id.
		/// </summary>
		public string id;

		/// <summary>
		/// Name of the user.
		/// </summary>
		public string name;

		/// <summary>
		/// URL of the organization's logo. Optional.
		/// </summary>
		public string logo;
	}

	/// <summary>
	/// <seealso href="https://docs.zeplin.dev/reference#organization_member"/>
	/// </summary>
	[System.Serializable]
	public struct OrganizationMember
	{
		/// <summary>
		/// Member.
		/// </summary>
		public User user;

		/// <summary>
		/// The role of the user in the organization.
		/// </summary>
		public Role role;

		/// <summary>
		/// Whether the user's membership is restricted to only the projects
		/// that they are member of.
		/// </summary>
		public bool restricted;
	}

	#endregion

	#region Project Models

	/// <summary>
	/// <seealso href="https://docs.zeplin.dev/reference#project"/>
	/// </summary>
	[System.Serializable]
	public struct Project
	{
		/// <summary>
		/// The unique id of the project.
		/// </summary>
		public string id;

		/// <summary>
		/// The name of the project.
		/// </summary>
		public string name;

		/// <summary>
		/// The description of the project. Optional.
		/// </summary>
		public string description;

		/// <summary>
		/// The target platform of the project.
		/// </summary>
		public Platform platform;

		/// <summary>
		/// URL of the project's thumbnail image. Optional.
		/// </summary>
		public string thumbnail;

		/// <summary>
		/// The status of the project.
		/// </summary>
		public Status status;

		/// <summary>
		/// Organization.
		/// </summary>
		public OrganizationSummary organization;

		/// <summary>
		/// URL of the project's scene (public projects only). Optional.
		/// </summary>
		public string sceneUrl;

		/// <summary>
		/// The unix timestamp when the project was created.
		/// </summary>
		public int created;

		/// <summary>
		/// The unix timestamp when the project was updated. Optional.
		/// </summary>
		public int updated;

		/// <summary>
		/// The number of members of the project.
		/// </summary>
		public int numberOfMembers;

		/// <summary>
		/// The number of screens in the project.
		/// </summary>
		public int numberOfScreens;

		/// <summary>
		/// The number of components exported to the project.
		/// </summary>
		public int numberOfComponents;

		/// <summary>
		/// The number of text styles added to the project.
		/// </summary>
		public int numberOfTextStyles;

		/// <summary>
		/// The number of colors added to the project.
		/// </summary>
		public int numberOfColors;

		/// <summary>
		/// Unique id of the style guide which the project is linked to.
		/// Optional.
		/// </summary>
		public Identifier linkedStyleguide;

		/// <summary>
		/// The status of the project.
		/// </summary>
		public enum Status
		{
			ACTIVE,
			ARCHIVED
		}
	}

	/// <summary>
	/// <seealso href="https://docs.zeplin.dev/reference#project_member"/>
	/// </summary>
	[System.Serializable]
	public struct ProjectMember
	{
		/// <summary>
		/// User.
		/// </summary>
		public User user;

		/// <summary>
		/// The role of the user in the project.
		/// </summary>
		public Role role;
	}

	#endregion

	#region Screen Models

	/// <summary>
	/// <seealso href="https://docs.zeplin.dev/reference#screen"/>
	/// </summary>
	[System.Serializable]
	public struct Screen
	{
		/// <summary>
		/// The unique id of the screen.
		/// </summary>
		public string id;

		/// <summary>
		/// The name of the screen.
		/// </summary>
		public string name;

		/// <summary>
		/// The description of the screen. Optional.
		/// </summary>
		public string description;

		/// <summary>
		/// The tags platform of the screen.
		/// </summary>
		public string[] tags;

		/// <summary>
		/// Screen Image.
		/// </summary>
		public Image image;

		/// <summary>
		/// The unix timestamp when the screen was created.
		/// </summary>
		public int created;

		/// <summary>
		/// The unix timestamp when the screen was updated. Optional.
		/// </summary>
		public int updated;

		/// <summary>
		/// The number of versions exported to the screen.
		/// </summary>
		public int numberOfVersions;

		/// <summary>
		/// The number of notes in the screen.
		/// </summary>
		public int numberOfNotes;

		/// <summary>
		/// Unique identifier of the section that contains the screen.
		/// Optional.
		/// </summary>
		public Identifier section;
	}

	/// <summary>
	/// <seealso href="https://docs.zeplin.dev/reference#screen_version"/>
	/// </summary>
	[System.Serializable]
	public struct ScreenVersion
	{
		/// <summary>
		/// The unique id of the version.
		/// </summary>
		public string id;

		/// <summary>
		/// Creator of the screen. Optional.
		/// </summary>
		public User creator;

		/// <summary>
		/// Information about the version commit. Optional.
		/// </summary>
		public VersionCommit versionCommit;

		/// <summary>
		/// URL of the image for the version. Optional.
		/// </summary>
		public string imageUrl;

		/// <summary>
		/// Source application of the version.
		/// </summary>
		public Source source;

		/// <summary>
		/// Width of the version.
		/// </summary>
		public int width;

		/// <summary>
		/// Height of the version.
		/// </summary>
		public int height;

		/// <summary>
		/// Color of the background. Optional.
		/// </summary>
		public SimpleColor backgroundColor;

		/// <summary>
		/// Pixel density.
		/// </summary>
		public float densityScale;

		/// <summary>
		/// Screen links.
		/// </summary>
		public Link[] links;

		/// <summary>
		/// Grid of the link. Optional.
		/// </summary>
		public Grid grid;

		/// <summary>
		/// Layers of the screen version.
		/// </summary>
		public Layer[] layers;

		/// <summary>
		/// Assets of the screen version.
		/// </summary>
		public Asset[] assets;

		/// <summary>
		/// The unix timestamp when the screen version was created.
		/// </summary>
		public int created;
	}

	/// <summary>
	/// <seealso href=
	/// "https://docs.zeplin.dev/reference#screen_version_summary"/>
	/// </summary>
	[System.Serializable]
	public struct ScreenVersionSummary
	{
		/// <summary>
		/// The unique id of the version.
		/// </summary>
		public string id;

		/// <summary>
		/// Creator of the summary version. Optional.
		/// </summary>
		public User creator;

		/// <summary>
		/// Commit of the Version. Optional.
		/// </summary>
		public VersionCommit commit;

		/// <summary>
		/// URL of the image for the version.
		/// </summary>
		public string imageUrl;

		/// <summary>
		/// Source application of the version.
		/// </summary>
		public Source source;

		/// <summary>
		/// Width of the version.
		/// </summary>
		public int width;

		/// <summary>
		/// Height of the version.
		/// </summary>
		public int height;

		/// <summary>
		/// Pixel density.
		/// </summary>
		public float densityScale;

		/// <summary>
		/// Links of the screen version.
		/// </summary>
		public Link[] links;

		/// <summary>
		/// The unix timestamp when the screen version summary was created.
		/// </summary>
		public int created;
	}

	/// <summary>
	/// <seealso creh="https://docs.zeplin.dev/reference#version_commit"/>
	/// </summary>
	[System.Serializable]
	public struct VersionCommit
	{
		/// <summary>
		/// Commit message for the version.
		/// </summary>
		public string message;

		/// <summary>
		/// Author of the commit. Optional.
		/// </summary>
		public User author;

		/// <summary>
		/// Color of the commit.
		/// </summary>
		public SimpleColor color;
	}

	/// <summary>
	/// <seealso creh="https://docs.zeplin.dev/reference#screen_note"/>
	/// </summary>
	[System.Serializable]
	public struct ScreenNote
	{
		/// <summary>
		/// The unique id of the note.
		/// </summary>
		public string id;

		/// <summary>
		/// Creator of the screen note.
		/// </summary>
		public User creator;

		/// <summary>
		/// Order of the note in the screen (e.g., 1, 2, 3, so on).
		/// </summary>
		public int order;

		/// <summary>
		/// Status of the note.
		/// </summary>
		public Status status;

		/// <summary>
		/// Position of the note with respect to top left corner. Values
		/// are normalized in [0, 1].
		/// </summary>
		public Position position;

		/// <summary>
		/// Color of the screen note.
		/// </summary>
		public SimpleColor color;

		/// <summary>
		/// Comments on note.
		/// </summary>
		public Comment[] comments;

		/// <summary>
		/// The unix timestamp when the note was created
		/// </summary>
		public int created;

		/// <summary>
		/// Status of the note.
		/// </summary>
		public enum Status
		{
			OPEN,
			RESOLVED
		}

		/// <summary>
		/// Position of the note with respect to top left corner. Values
		/// are normalized in [0, 1]
		/// </summary>

		[System.Serializable]
		public struct Position
		{
			public float x;
			public float y;
		}
	}

	/// <summary>
	/// <seealso href="https://docs.zeplin.dev/reference#screen_section"/>
	/// </summary>
	[System.Serializable]
	public struct ScreenSection
	{
		/// <summary>
		/// The unique id of the screen section.
		/// </summary>
		public string id;

		/// <summary>
		/// The unix timestamp when the screen section was created.
		/// </summary>
		public int created;

		/// <summary>
		/// The name of the screen section.
		/// </summary>
		public string name;

		/// <summary>
		/// The description of the screen section. Optional.
		/// </summary>
		public string description;
	}

	#endregion

	#region Style Models

	/// <summary>
	/// <seealso creh="https://docs.zeplin.dev/reference#styleguide"/>
	/// </summary>
	[System.Serializable]
	public struct Styleguide
	{
		/// <summary>
		/// The unique id of the style guide.
		/// </summary>
		public string id;

		/// <summary>
		/// The name of the style guide.
		/// </summary>
		public string name;

		/// <summary>
		/// The description of the style guide (It can contain <seealso href=
		/// "https://zpl.io/article/markdown-support">markdown</seealso>).
		/// Optional.
		/// </summary>
		public string description;

		/// <summary>
		/// The target platform of the style guide.
		/// </summary>
		public Platform platform;

		/// <summary>
		/// URL of the style guides thumbnail image. Optional.
		/// </summary>
		public string thumbnail;

		/// <summary>
		/// The status of the style guide.
		/// </summary>
		public Status status;

		/// <summary>
		/// Organization. Optional.
		/// </summary>
		public OrganizationSummary organization;

		/// <summary>
		/// The unix timestamp when the style guide was created
		/// </summary>
		public int created;

		/// <summary>
		/// The unix timestamp when the style guide was updated. Optional.
		/// </summary>
		public int updated;

		/// <summary>
		/// The number of members of the style guide.
		/// </summary>
		public int numberOfMembers;

		/// <summary>
		/// The number of components exported to the style guide.
		/// </summary>
		public int numberOfComponents;

		/// <summary>
		/// The number of text styles added to the style guide.
		/// </summary>
		public int numberOfTextStyles;

		/// <summary>
		/// The number of colors added to the style guide.
		/// </summary>
		public int numberOfColors;

		/// <summary>
		/// ID of the parent. Optional.
		/// </summary>
		public Identifier parent;

		/// <summary>
		/// The status of the style guide.
		/// </summary>
		public enum Status
		{
			ACTIVE,
			ARCHIVED
		}
	}

	/// <summary>
	/// <seealso href="https://docs.zeplin.dev/reference#styleguide_member"/>
	/// </summary>
	[System.Serializable]
	public struct SyleguideMember
	{
		/// <summary>
		/// Member.
		/// </summary>
		public User user;

		/// <summary>
		/// The role of the user in the style guide.
		/// </summary>
		public Role role;
	}

	/// <summary>
	/// <seealso href="https://docs.zeplin.dev/reference#color"/>
	/// </summary>
	[System.Serializable]
	public struct Color
	{
		/// <summary>
		/// Identifier of the color.
		/// </summary>
		public string id;

		/// <summary>
		/// The unix timestamp when the color was created.
		/// </summary>
		public int created;

		/// <summary>
		/// Name of the color.
		/// </summary>
		public string name;

		/// <summary>
		/// Red component of the color
		/// </summary>
		public int r;

		/// <summary>
		/// Green component of the color.
		/// </summary>
		public int g;

		/// <summary>
		/// Blue component of the color
		/// </summary>
		public int b;

		/// <summary>
		/// Alpha component of the color
		/// </summary>
		public float a;
	}

	/// <summary>
	/// <seealso href="https://docs.zeplin.dev/reference#text_style"/>
	/// </summary>
	[System.Serializable]
	public struct TextStyle
	{
		/// <summary>
		/// Identifier of the text style.
		/// </summary>
		public string id;

		/// <summary>
		/// Name of the text style.
		/// </summary>
		public string name;

		/// <summary>
		/// The unix timestamp when the text style was created.
		/// </summary>
		public int created;

		/// <summary>
		/// PostScript name of the text style, e.g. Roboto-Regular.
		/// </summary>
		public string postscriptName;

		/// <summary>
		/// Font family of the text style, e.g. Roboto, Arial.
		/// </summary>
		public string fontFamily;

		/// <summary>
		/// Font size of the text style.
		/// </summary>
		public float fontSize;

		/// <summary>
		/// Font weight of the text style, e.g. 500, 700.
		/// </summary>
		public float fontWeight;

		/// <summary>
		/// Font style of the text style, e.g. italic, oblique.
		/// </summary>
		public string fontStyle;

		/// <summary>
		/// Font stretch form of the text style, e.g. 0.75, 1.00.
		/// </summary>
		public float fontStretch;

		/// <summary>
		/// Minimum height of a line for the text style. Optional.
		/// </summary>
		public float lineHeight;

		/// <summary>
		/// Spacing between letters. Optional.
		/// </summary>
		public float letterSpacing;

		/// <summary>
		/// Horizontal alignment of the text. Optional.
		/// </summary>
		public TextAlign textAlign;

		/// <summary>
		/// Color of the text. Optional.
		/// </summary>
		public SimpleColor color;

	}

	#endregion

	#region Component Models

	/// <summary>
	/// <seealso href="https://docs.zeplin.dev/reference#component_section"/>
	/// </summary>
	[System.Serializable]
	public struct ComponentSection
	{
		/// <summary>
		/// The unique id of the component section.
		/// </summary>
		public string id;

		/// <summary>
		/// The unix timestamp when the component section was created.
		/// </summary>
		public int created;

		/// <summary>
		/// The name of the component section.
		/// </summary>
		public string name;

		/// <summary>
		/// The description of the component section. Optional.
		/// </summary>
		public string description;

		/// <summary>
		/// Groups of component section.
		/// </summary>
		public ComponentGroup[] groups;
	}

	/// <summary>
	/// <seealso href="https://docs.zeplin.dev/reference#component_group"/>
	/// </summary>
	[System.Serializable]
	public struct ComponentGroup
	{
		/// <summary>
		/// The unique id of the component section.
		/// </summary>
		public string id;

		/// <summary>
		/// The unix timestamp when the component group was created.
		/// </summary>
		public int created;

		/// <summary>
		/// The name of the component section.
		/// </summary>
		public string name;

		/// <summary>
		/// The description of the component section. Optional.
		/// </summary>
		public string description;
	}

	/// <summary>
	/// <seealso href="https://docs.zeplin.dev/reference#component"/>
	/// </summary>
	[System.Serializable]
	public struct Component
	{
		/// <summary>
		/// The unique id of the component.
		/// </summary>
		public string id;

		/// <summary>
		/// The name of the component.
		/// </summary>
		public string name;

		/// <summary>
		/// The description of the component. Optional.
		/// </summary>
		public string description;

		/// <summary>
		/// Image of the component.
		/// </summary>
		public Image image;

		/// <summary>
		/// The unix timestamp when the component was created.
		/// </summary>
		public int created;

		/// <summary>
		/// The unix timestamp when the component was updated. Optional.
		/// </summary>
		public int updated;

		/// <summary>
		/// Section of the component. Optional.
		/// </summary>
		public Section section;

		/// <summary>
		/// Section of the component.
		/// </summary>

		[System.Serializable]
		public struct Section
		{
			/// <summary>
			/// Unique id of the section that contains the component.
			/// </summary>
			public string id;

			/// <summary>
			/// Unique identifier of the group that contains the component.
			/// Optional.
			/// </summary>
			public Identifier group;
		}
	}

	/// <summary>
	/// <seealso href="https://docs.zeplin.dev/reference#component_version"/>
	/// </summary>
	[System.Serializable]
	public struct ComponentVersion
	{
		/// <summary>
		/// The unique id of the version.
		/// </summary>
		public string id;

		/// <summary>
		/// Creator of the component version. Optional.
		/// </summary>
		public User creator;

		/// <summary>
		/// URL of the image for the version. Optional.
		/// </summary>
		public string imageUrl;

		/// <summary>
		/// Source application of the version.
		/// </summary>
		public Source source;

		/// <summary>
		/// Width of the version.
		/// </summary>
		public int width;

		/// <summary>
		/// Height of the version.
		/// </summary>
		public int height;

		/// <summary>
		/// Background color of the component.
		/// </summary>
		public SimpleColor backgroundColor;

		/// <summary>
		/// Pixel density.
		/// </summary>
		public float densityScale;

		/// <summary>
		/// Links of the component version.
		/// </summary>
		public Link[] links;

		/// <summary>
		/// Grid of the component. Optional.
		/// </summary>
		public Grid grid;

		/// <summary>
		/// Layers of the component version.
		/// </summary>
		public Layer[] layers;

		/// <summary>
		/// Assets of the component version.
		/// </summary>
		public Asset[] assets;

		/// <summary>
		/// The unix timestamp when the component version was created.
		/// </summary>
		public int created;
	}

	#endregion

	#region Layer Models

	/// <summary>
	/// <seealso creh="https://docs.zeplin.dev/reference#layer"/>
	/// </summary>
	[System.Serializable]
	public struct Layer
	{
		/// <summary>
		/// Layer's unique id.
		/// </summary>
		public string id;

		/// <summary>
		/// Layer's identifier in the design tool. Optional.
		/// </summary>
		public string sourceId;

		/// <summary>
		/// Type of the layer.
		/// </summary>
		public Type type;

		/// <summary>
		/// Name of the layer. Optional.
		/// </summary>
		public string name;

		/// <summary>
		/// Bounding rectangle of the layer.
		/// </summary>
		public Rect rect;

		/// <summary>
		/// Fills applied to the layer. Optional.
		/// </summary>
		public LayerFill[] fills;

		/// <summary>
		/// Borders of the layer.
		/// </summary>
		public Border borders;

		/// <summary>
		/// Shadows applied to the layer.
		/// </summary>
		public Shadow shadows;

		/// <summary>
		/// Blur applied to the layer. Optional.
		/// </summary>
		public Blur blur;

		/// <summary>
		/// Opacity of the layer, [0, 1].
		/// </summary>
		public float opacity;

		/// <summary>
		/// Blend Mode. Optional.
		/// </summary>
		public BlendMode blendMode;

		/// <summary>
		/// Border radius of the layer. Optional.
		/// </summary>
		public float borderRadius;

		/// <summary>
		/// Rotation of the layer. Optional.
		/// </summary>
		public float rotation;

		/// <summary>
		/// Indicates whether the layer has assets or not. Optional.
		/// </summary>
		public bool exportable;

		/// <summary>
		/// Text of the text layer. Optional.
		/// </summary>
		public string content;

		/// <summary>
		/// Text Styles.
		/// </summary>
		public TextStyle textStyles;

		/// <summary>
		/// Additional layers. Optional.
		/// </summary>
		public Layer[] layers;

		/// <summary>
		/// Name of the component the group layer is referencing. Optional.
		/// </summary>
		public string componentName;

		/// <summary>
		/// Type of the layer.
		/// </summary>
		public enum Type
		{
			TEXT,
			SHAPE,
			GROUP
		}

		/// <summary>
		/// Text Styles.
		/// </summary>

		[System.Serializable]
		public struct TextStyle
		{
			/// <summary>
			/// Text style range.
			/// </summary>
			public Range range;

			/// <summary>
			/// Layer of the text style.
			/// </summary>
			public LayerTextStyle style;


			[System.Serializable]
			public struct Range
			{
				/// <summary>
				/// Start of the range
				/// </summary>
				public float location;

				/// <summary>
				/// Length of the range.
				/// </summary>
				public float length;
			}
		}
	}

	/// <summary>
	/// <seealso href="https://docs.zeplin.dev/reference#blend_mode"/>
	/// </summary>
	public enum BlendMode
	{
		NORMAL,
		DARKEN,
		MULTIPLY,
		COLOR_BURN,
		LIGHTEN,
		SCREEN,
		COLOR_DODGE,
		OVERLAY,
		SOFT_LIGHT,
		HARD_LIGHT,
		DIFFERENCE,
		EXCLUSION,
		HUE,
		SATURATION,
		COLOR,
		LUMINOSITY,
		SOURCE_IN,
		SOURCE_OUT,
		SOURCE_ATOP,
		DESTINATION_OVER,
		DESTINATION_IN,
		DESTINATION_OUT,
		DESTINATION_ATOP,
		DISSOLVE,
		LINEAR_BURN,
		LINEAR_DODGE,
		DARKER_COLOR,
		LIGHTER_COLOR,
		VIVID_LIGHT,
		LINEAR_LIGHT,
		PIN_LIGHT,
		HARD_MIX,
		SUBTRACT,
		DIVIDE
	}

	/// <summary>
	/// Border of a layer.
	/// </summary>
	[System.Serializable]
	public struct Border
	{
		/// <summary>
		/// Position of the border. Optional.
		/// </summary>
		public Position position;

		/// <summary>
		/// Thickness of the border. Optional.
		/// </summary>
		public float thickness;

		/// <summary>
		/// Fill of the border. Optional.
		/// </summary>
		public LayerFill fill;

		/// <summary>
		/// Position of the border.
		/// </summary>
		public enum Position
		{
			CENTER,
			INSIDE,
			OUTSIDE
		}
	}

	/// <summary>
	/// Shadow of a layer.
	/// </summary>
	[System.Serializable]
	public struct Shadow
	{
		/// <summary>
		/// Type of the shadow. Optional.
		/// </summary>
		public Type type;

		/// <summary>
		/// Horizontal offset of the shadow. Optional.
		/// </summary>
		public float offsetX;

		/// <summary>
		/// Vertical offset of the shadow. Optional.
		/// </summary>
		public float offsetY;

		/// <summary>
		/// Blur radius of the shadow. Optional.
		/// </summary>
		public float blurRadius;

		/// <summary>
		/// Spread of the shadow. Optional.
		/// </summary>
		public float spread;

		/// <summary>
		/// Color of the layer. Optional.
		/// </summary>
		public LayerColor color;

		/// <summary>
		/// Type of the shadow.
		/// </summary>
		public enum Type
		{
			OUTER,
			INNER
		}
	}

	/// <summary>
	/// Blur of a layer.
	/// </summary>
	[System.Serializable]
	public struct Blur
	{
		/// <summary>
		/// Type of the blur. Optional.
		/// </summary>
		public Type type;

		/// <summary>
		/// Radius of the blur. Optional.
		/// </summary>
		public float radius;

		/// <summary>
		/// Type of the blur.
		/// </summary>
		public enum Type
		{
			GAUSSIAN,
			BACKGROUND
		}
	}

	/// <summary>
	/// <seealso href="https://docs.zeplin.dev/reference#asset"/>
	/// </summary>
	[System.Serializable]
	public struct Asset
	{
		/// <summary>
		/// Layer's identifier in the design tool. Optional.
		/// </summary>
		public string layerSourceId;

		/// <summary>
		/// Display name of the asset.
		/// </summary>
		public string displayName;

		/// <summary>
		/// Name of the layer containing the asset. Optional.
		/// </summary>
		public string layerName;

		/// <summary>
		/// Content of asset.
		/// </summary>
		public Content[] contents;
	}

	/// <summary>
	/// <seealso href="https://docs.zeplin.dev/reference#layer_color"/>
	/// </summary>
	[System.Serializable]
	public struct LayerColor
	{
		/// <summary>
		/// Name of the color. Optional.
		/// </summary>
		public string name;

		/// <summary>
		/// Red component of the color.
		/// </summary>
		public int r;

		/// <summary>
		/// Green component of the color.
		/// </summary>
		public int g;

		/// <summary>
		/// Blue component of the color.
		/// </summary>
		public int b;

		/// <summary>
		/// Alpha component of the color.
		/// </summary>
		public float a;
	}

	/// <summary>
	/// <seealso href="https://docs.zeplin.dev/reference#layer_fill"/>
	/// </summary>
	[System.Serializable]
	public struct LayerFill
	{
		/// <summary>
		/// Type of the fill.
		/// </summary>
		public Type type;

		/// <summary>
		/// Color of the layer. Optional.
		/// </summary>
		public LayerColor color;

		/// <summary>
		/// Gradient of the fill. Optional.
		/// </summary>
		public Gradient gradient;

		/// <summary>
		/// Blend mode. Optional.
		/// </summary>
		public BlendMode blendMode;

		/// <summary>
		/// Type of the fill.
		/// </summary>
		public enum Type
		{
			COLOR,
			GRADIENT
		}
	}

	/// <summary>
	/// <seealso href="https://docs.zeplin.dev/reference#layer_text_style"/>
	/// </summary>
	[System.Serializable]
	public struct LayerTextStyle
	{
		/// <summary>
		/// PostScript name of the text style, e.g. Roboto-Regular.
		/// </summary>
		public string postscriptName;

		/// <summary>
		/// Font family of the text style, e.g. Roboto, Arial.
		/// </summary>
		public string fontFamily;

		/// <summary>
		/// Font size of the text style.
		/// </summary>
		public float fontSize;

		/// <summary>
		/// Font weight of the text style, e.g. 500, 700.
		/// </summary>
		public float fontWeight;

		/// <summary>
		/// Font style of the text style, e.g. italic, oblique.
		/// </summary>
		public string fontStyle;

		/// <summary>
		/// Font stretch form of the text style, e.g. 0.75, 1.00.
		/// </summary>
		public float fontStretch;

		/// <summary>
		/// Minimum height of a line for the text style. Optional.
		/// </summary>
		public float lineHeight;

		/// <summary>
		/// Spacing between letters. Optional.
		/// </summary>
		public float letterSpacing;

		/// <summary>
		/// Horizontal alignment of the text style, left, right, center
		/// or justify. Optional.
		/// </summary>
		public TextAlign textAlign;

		/// <summary>
		/// Color of the layer text style. Optional.
		/// </summary>
		public SimpleColor color;
	}

	/// <summary>
	/// Content.
	/// </summary>
	[System.Serializable]
	public struct Content
	{
		/// <summary>
		/// URL of the asset content.
		/// </summary>
		public string url;

		/// <summary>
		/// Format of the asset content.
		/// </summary>
		public Format format;

		/// <summary>
		/// Density of the asset content. Optional.
		/// </summary>
		public float density;

		/// <summary>
		/// Format of the asset content.
		/// </summary>
		public enum Format
		{
			PNG,
			JPG,
			SVG,
			PDF
		}
	}

	/// <summary>
	/// Gradient.
	/// </summary>
	[System.Serializable]
	public struct Gradient
	{
		/// <summary>
		/// Type of the gradient. Optional.
		/// </summary>
		public Type type;

		/// <summary>
		/// Angle of the gradient. Optional.
		/// </summary>
		public float angle;

		/// <summary>
		/// Scale of the gradient. Optional.
		/// </summary>
		public float scale;

		/// <summary>
		/// Stops af a color.
		/// </summary>
		public ColorStops colorStops;

		/// <summary>
		/// Opacity of gradient. Optional.
		/// </summary>
		public float opacity;

		public enum Type
		{
			LINEAR,
			RADIAL,
			ANGULAR
		}

		/// <summary>
		/// Stops af a color.
		/// </summary>

		[System.Serializable]
		public struct ColorStops
		{
			/// <summary>
			/// Color of the layer.
			/// </summary>
			public LayerColor color;

			/// <summary>
			/// Position.
			/// </summary>
			public float position;
		}
	}


	#endregion

	#region Spacing Models

	/// <summary>
	/// <seealso href="https://docs.zeplin.dev/reference#spacing_section"/>
	/// </summary>
	[System.Serializable]
	public struct SpacingSection
	{
		/// <summary>
		/// The unique id of the section.
		/// </summary>
		public string id;

		/// <summary>
		/// The name of the section.
		/// </summary>
		public string name;

		/// <summary>
		/// The description of the section. Optional.
		/// </summary>
		public string description;

		/// <summary>
		/// Unique id of the base token of the section.
		/// </summary>
		public Identifier baseToken;
	}

	/// <summary>
	/// <seealso href="https://docs.zeplin.dev/reference#spacing_token"/>
	/// </summary>
	[System.Serializable]
	public struct SpacingToken
	{
		/// <summary>
		/// The unique id of the token
		/// </summary>
		public string id;

		/// <summary>
		/// The name of the token.
		/// </summary>
		public string name;

		/// <summary>
		/// The value of the token.
		/// </summary>
		public float number;

		/// <summary>
		/// Color of the spacing token.
		/// </summary>
		public SimpleColor color;

		/// <summary>
		/// Unique id of the section that contains the token.
		/// </summary>
		public Identifier section;
	}

	#endregion

	#region General Models

	/// <summary>
	/// Model for a general identifier.
	/// </summary>
	[System.Serializable]
	public struct Identifier
	{
		public string id;
	}

	/// <summary>
	/// Source application of the version.
	/// </summary>
	public enum Source
	{
		FIGMA,
		SKETCH,
		XD,
		PSD,
		BITMAP
	}

	/// <summary>
	/// The target platform of the project.
	/// </summary>
	public enum Platform
	{
		BASE,
		WEB,
		IOS,
		ANDROID,
		MAC_OS
	}

	/// <summary>
	/// Horizontal alignment of the text style, left, right, center,
	/// or justify. Optional.
	/// </summary>
	public enum TextAlign
	{
		LEFT,
		RIGHT,
		CENTER,
		JUSTIFY
	}

	/// <summary>
	/// Screen links.
	/// </summary>
	[System.Serializable]
	public struct Link
	{
		/// <summary>
		/// Bounding rectangle of the link's hot spot.
		/// </summary>
		public Rect rect;

		/// <summary>
		/// Destination of the link.
		/// </summary>
		public Destination destination;

		/// <summary>
		/// Destination of a link.
		/// </summary>

		[System.Serializable]
		public struct Destination
		{
			/// <summary>
			/// Destination name.
			/// </summary>
			public string name;

			/// <summary>
			/// Type of the link (screen or previous).
			/// </summary>
			public Type type;

			/// <summary>
			/// Type of the link (screen or previous).
			/// </summary>
			public enum Type
			{
				SCREEN,
				PREVIOUS
			}
		}
	}

	/// <summary>
	/// Rect.
	/// </summary>
	[System.Serializable]
	public struct Rect
	{
		public int width;
		public int height;
		public int x;
		public int y;
	}

	/// <summary>
	/// Simple representation of a color.
	/// </summary>
	[System.Serializable]
	public struct SimpleColor
	{
		/// <summary>
		/// Red component.
		/// </summary>
		public int r;

		/// <summary>
		/// Green component.
		/// </summary>
		public int g;

		/// <summary>
		/// Blue component.
		/// </summary>
		public int b;

		/// <summary>
		/// Alpha component.
		/// </summary>
		public float a;
	}


	/// <summary>
	/// Image References.
	/// </summary>
	[System.Serializable]
	public struct Image
	{
		/// <summary>
		/// Width of the screen image.
		/// </summary>
		public string width;

		/// <summary>
		/// Height of the screen image.
		/// </summary>
		public string height;

		/// <summary>
		/// URL of the original image for the screen (from the latest version).
		/// </summary>
		public string originalUrl;
	}

	/// <summary>
	/// Grid.
	/// </summary>
	[System.Serializable]
	public struct Grid
	{
		/// <summary>
		/// Horizontal offset.
		/// </summary>
		public float horizontalOffset;

		/// <summary>
		/// Vertical.
		/// </summary>
		public Vertical vertical;

		/// <summary>
		/// Horizontal.
		/// </summary>
		public Horizontal horizontal;

		/// <summary>
		/// Vertical.
		/// </summary>

		[System.Serializable]
		public struct Vertical
		{
			/// <summary>
			/// Gutter Width.
			/// </summary>
			public float gutterWidth;

			/// <summary>
			/// Width of the column.
			/// </summary>
			public float columnWidth;

			/// <summary>
			/// Number of colors.
			/// </summary>
			public float numberOfColors;

			/// <summary>
			/// Whether the gutter is on outside.
			/// </summary>
			public bool guttersOnOutside;
		}

		/// <summary>
		/// Horizontal.
		/// </summary>

		[System.Serializable]
		public struct Horizontal
		{
			/// <summary>
			/// Gutter Height.
			/// </summary>
			public float gutterHeight;

			/// <summary>
			/// Height of the row.
			/// </summary>
			public float rowHeight;
		}
	}

	/// <summary>
	/// Comment.
	/// </summary>
	[System.Serializable]
	public struct Comment
	{
		/// <summary>
		/// Unique id of the comment.
		/// </summary>
		public string id;

		/// <summary>
		/// Content of the comment.
		/// </summary>
		public string content;

		/// <summary>
		/// Author of the comment.
		/// </summary>
		public User author;

		/// <summary>
		/// The unix timestamp when the comment was updated.
		/// </summary>
		public int updated;
	}

	#endregion
}