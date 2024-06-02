using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ContactListTests;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using ContactBE.Models.Dtos;
using ContactBE.Models;
using System.Net.Http.Json;
using System.Net;
//using Microsoft.AspNetCore.Mvc.Testing.Program;


public class ContactControllerTest : IClassFixture<CutomWebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public ContactControllerTest(CutomWebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task GetContactsTests()
    {
        // Arrange
        var request = "/v1/contacts";

        // Act
        var response = await _client.GetAsync(request);

        // Assert
        response.EnsureSuccessStatusCode();
        var contacts = await response.Content.ReadFromJsonAsync<ContactData[]>();
        Assert.NotNull(contacts);
    }

    [Fact]
    public async Task CreateContactTests()
    {
        // Arrange
        var request = "/v1/contacts";
        var newContact = new CreateContactDto
        {
            Name = "John Doe",
            Number = "1234567890"
        };

        // Act
        var response = await _client.PostAsJsonAsync(request, newContact);

        // Assert
        response.EnsureSuccessStatusCode();
        var createdContactId = await response.Content.ReadFromJsonAsync<Guid>();
        Assert.NotEqual(Guid.Empty, createdContactId);
    }

    [Fact]
    public async Task GetContactByIdTest()
    {
        // Arrange
        var newContact = new CreateContactDto
        {
            Name = "Jane Doe",
            Number = "0987654321"
        };
        var createResponse = await _client.PostAsJsonAsync("/v1/contacts", newContact);
        createResponse.EnsureSuccessStatusCode();
        var createdContactId = await createResponse.Content.ReadFromJsonAsync<Guid>();

        // Act
        var response = await _client.GetAsync($"/v1/contacts/{createdContactId}");

        // Assert
        response.EnsureSuccessStatusCode();
        var contact = await response.Content.ReadFromJsonAsync<ContactData>();
        Assert.NotNull(contact);
        Assert.Equal("Jane Doe", contact.Name);
        Assert.Equal("0987654321", contact.Number);
    }

    [Fact]
    public async Task UpdateContactTest()
    {
        // Arrange
        var newContact = new CreateContactDto
        {
            Name = "John Smith",
            Number = "1112223333"
        };
        var createResponse = await _client.PostAsJsonAsync("/v1/contacts", newContact);
        createResponse.EnsureSuccessStatusCode();
        var createdContactId = await createResponse.Content.ReadFromJsonAsync<Guid>();

        var updatedContact = new ContactData
        {
            Id = createdContactId,
            Name = "John Smith Updated",
            Number = "9998887777"
        };

        // Act
        var updateResponse = await _client.PutAsJsonAsync("/v1/contacts", updatedContact);

        // Assert
        updateResponse.EnsureSuccessStatusCode();

        var getResponse = await _client.GetAsync($"/v1/contacts/{createdContactId}");
        getResponse.EnsureSuccessStatusCode();
        var contact = await getResponse.Content.ReadFromJsonAsync<ContactData>();
        Assert.NotNull(contact);
        Assert.Equal("John Smith Updated", contact.Name);
        Assert.Equal("9998887777", contact.Number);
    }

    [Fact]
    public async Task DeleteContactTest()
    {
        // Arrange
        var newContact = new CreateContactDto
        {
            Name = "Delete Test",
            Number = "5554443333"
        };
        var createResponse = await _client.PostAsJsonAsync("/v1/contacts", newContact);
        createResponse.EnsureSuccessStatusCode();
        var createdContactId = await createResponse.Content.ReadFromJsonAsync<Guid>();

        // Act
        var deleteResponse = await _client.DeleteAsync($"/v1/contacts/{createdContactId}");

        // Assert
        deleteResponse.EnsureSuccessStatusCode();

        var getResponse = await _client.GetAsync($"/v1/contacts/{createdContactId}");
        Assert.Equal(HttpStatusCode.NoContent, getResponse.StatusCode);
    }

    [Fact]
    public async Task GetDefaultContactsTest()
    {
        // Arrange
        var request = "/v1/contacts/default";

        // Act
        var response = await _client.GetAsync(request);

        // Assert
        response.EnsureSuccessStatusCode();
        var defaultContacts = await response.Content.ReadFromJsonAsync<ContactData[]>();
        Assert.NotNull(defaultContacts);
        Assert.NotEmpty(defaultContacts);
    }

}

